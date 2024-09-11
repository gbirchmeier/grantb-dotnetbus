using Confluent.Kafka;
using EntityLib.Entities;
using ProtoBuf;

namespace OrderConsole;

public class Consumer {
    private const string _topic = "trade-status";
    private ConsumerConfig _config;

    public Consumer() {
        _config = new ConsumerConfig
        {
            // User-specific properties that you must set
            BootstrapServers = "localhost:9092",
            // Fixed properties
            GroupId         = "my-group-id",
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
            consumer.Subscribe(_topic);
            try {
                Console.WriteLine("Event consumer is running...");
                while (true) {
                    ConsumeResult<string,string> cr = consumer.Consume(cts.Token);
                    //Console.WriteLine($"Consumed event from topic {cr.Topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");

                    string key = cr.Message.Key;
                    byte[] data = Convert.FromBase64String(cr.Message.Value);
                    using (MemoryStream stream = new MemoryStream(data)) {
                        var statusUpdate = Serializer.Deserialize<TradeStatusUpdate>(stream);
                        Console.WriteLine($"<<< Received TradeStatusUpdate (key={key}): {statusUpdate.TradeSourceId} {statusUpdate.TradeStatus}");
                        Console.Write("> ");
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
