using System;
using Confluent.Kafka;

namespace DemoSender;

public static class Program {
    private static void Main(string[] args) {
        if (args.Length < 1)
            throw new InvalidOperationException("Need a param, which is the msg to send");

        const string topic = "demo-topic";

        var config = new ProducerConfig
        {
            // User-specific properties that you must set
            BootstrapServers = "localhost:9092",

            // Fixed properties
            Acks = Acks.All
        };

        var msg = new Message<string, string> { Key = "defaultKey", Value = args.First() };

        using (var producer = new ProducerBuilder<string, string>(config).Build()) {
            producer.Produce(topic, msg,
                (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError) {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    } else {
                        Console.WriteLine($"Produced event to topic {topic}: '{msg.Value}'");
                    }
                });
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}

