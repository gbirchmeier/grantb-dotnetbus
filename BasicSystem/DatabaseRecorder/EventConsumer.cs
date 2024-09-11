using Confluent.Kafka;
using Dapper;
using EntityLib.Entities;
using Microsoft.Data.SqlClient;
using ProtoBuf;

namespace DatabaseRecorder;

public class EventConsumer {
    private static readonly string[] _topics = ["trade-request", "trade-status"];
    private readonly ConsumerConfig _config;

    public static readonly string CxnString =
        "Server=127.0.0.1;Database=DotNetBus;User=sa;Password=Password123#;Encrypt=False";

    public EventConsumer() {
        _config = new ConsumerConfig
        {
            // User-specific properties that you must set
            BootstrapServers = "localhost:9092",
            // Fixed properties
            GroupId         = "db-recorder-id", // dunno what this is
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public void Run() {
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true;
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<string, string>(_config).Build())
        {
            consumer.Subscribe(_topics);
            Console.WriteLine(">> Waiting to consume...");
            try {
                while (true) {
                    ConsumeResult<string,string> cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from topic {cr.Topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");

                    //string key = cr.Message.Key;
                    byte[] data = Convert.FromBase64String(cr.Message.Value);

                    using (MemoryStream stream = new MemoryStream(data)) {
                        switch (cr.Topic) {
                            case "trade-request":
                                var tradeRequest = Serializer.Deserialize<TradeRequest>(stream);
                                Console.WriteLine($"* Received TradeRequest: {tradeRequest.SourceId} {tradeRequest.Symbol} {tradeRequest.Side} {tradeRequest.Price}");
                                using (var cxn = new SqlConnection(CxnString)) {
                                    cxn.Execute(TradeRequest.InsertTemplate, tradeRequest);
                                }
                                break;
                            case "trade-status":
                                var tradeStatusUpdate = Serializer.Deserialize<TradeStatusUpdate>(stream);
                                Console.WriteLine($"* Received TradeStatusUpdate: {tradeStatusUpdate.TradeSourceId} {tradeStatusUpdate.TradeStatus}");
                                using (var cxn = new SqlConnection(CxnString)) {
                                    cxn.Execute(TradeStatusUpdate.InsertTemplate, tradeStatusUpdate);
                                }
                                break;
                            default:
                                throw new ApplicationException($"Unsupported topic: {cr.Topic}");
                        }
                    }
                }
            }
            catch (OperationCanceledException) {
                // Ctrl-C was pressed.
            }
            finally{
                consumer.Close();
            }
        }
    }
}
