using System;
using System.Threading;
using Confluent.Kafka;

namespace DemoConsumer;

public static class Program {

    private static void Main(string[] args) {
        const string topic = "demo-topic";

        var config = new ConsumerConfig
        {
            // User-specific properties that you must set
            BootstrapServers = "localhost:9092",

            // Fixed properties
            GroupId         = "my-group-id",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true; // prevent the process from terminating.
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe(topic);
            Console.WriteLine("Waiting to consume...");
            try {
                while (true) {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
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
