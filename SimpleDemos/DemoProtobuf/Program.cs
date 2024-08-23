using DemoProtobuf.Models;
using ProtoBuf;

namespace DemoProtobuf;

public static class Program {

    private static void Main(string[] args) {
        Console.WriteLine("DemoProtobuf test program:");

        Band spoon = new()
        {
            Name = "Spoon",
            Genre = EnumGenre.Rock,
            Members = [
                new Musician() { Name = "Britt Daniel" }
            ]
        };

        Band rtj = new()
        {
            Name = "Run The Jewels",
            Genre = EnumGenre.HipHop,
            Members =
            [
                new Musician() { Name = "El-P" },
                new Musician() { Name = "Killer Mike" }
            ]
        };

        using (MemoryStream ms = new()) {
            Console.WriteLine("Serializing/Deserializing bands...");

            Console.WriteLine($"* ser: {spoon.Name}");
            Serializer.Serialize(ms, spoon);
            ms.Position = 0;
            var out1 = Serializer.Deserialize<Band>(ms);
            Console.WriteLine($"  deser: {out1.Name}");

            Console.WriteLine($"* ser: {rtj.Name}");
            Serializer.Serialize(ms, rtj);
            ms.Position = 0;
            out1 = Serializer.Deserialize<Band>(ms);
            Console.WriteLine($"  deser: {out1.Name}");
        }
    }
}

