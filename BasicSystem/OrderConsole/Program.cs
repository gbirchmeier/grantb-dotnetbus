using System;
using System.Threading;
using Confluent.Kafka;

namespace OrderConsole;

public static class Program {
    private static void Main(string[] args) {
        if (args.Length < 1)
            throw new InvalidOperationException("Need a param, which is the trader's name");

        string username = args.First();



        var config = new ProducerConfig
        {
            // User-specific properties that you must set
            BootstrapServers = "localhost:9092",

            // Fixed properties
            Acks = Acks.All
        };

        Consumer consumer = new Consumer();
        Thread t = new Thread(consumer.Run);
        t.Start();

        Producer producer = new Producer(username);
        producer.Run();

    }
}
