using Confluent.Kafka;
using EntityLib.Entities;
using ProtoBuf;

namespace OrderConsole;

public class Producer {
   private const string _topic = "trade-request";
   private readonly string _traderId;

   private readonly IProducer<string,string> _producer;

   public Producer(string traderId) {
      _traderId = traderId;
      var config = new ProducerConfig
      {
         // User-specific properties that you must set
         BootstrapServers = "localhost:9092",

         // Fixed properties
         Acks = Acks.All
      };

      _producer = new ProducerBuilder<string, string>(config).Build();
   }

   public void Run() {
      while (true) {
         Console.WriteLine("To send an order, enter e.g. 'buy <symbol>)'");
         Console.Write("> ");
         string inp = Console.ReadLine()!;

         if (inp is null) {
            Console.WriteLine("Exiting...");
            System.Environment.Exit(0);
         }

         var inparts = inp.Split();
         SideEnum side = inparts[0].ToLower().Contains("buy") ? SideEnum.Buy : SideEnum.Sell;
         string symbol = inparts[1];

         TradeRequest req = new(_traderId, "OrderConsole", OrderTypeEnum.Market, symbol, side);

         using (MemoryStream stream = new MemoryStream())
         {
            Serializer.Serialize(stream, req);
            string serialized = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);

            var msg = new Message<string, string> { Key = _traderId, Value = serialized };
            _producer.Produce(_topic, msg,
               (deliveryReport) =>
               {
                  if (deliveryReport.Error.Code != ErrorCode.NoError) {
                     Console.WriteLine($"Failed to send order: {deliveryReport.Error.Reason}");
                  } else {
                     Console.WriteLine("Sent order");
                  }
               });
            _producer.Flush(TimeSpan.FromSeconds(10));
         }
      }
   }
}
