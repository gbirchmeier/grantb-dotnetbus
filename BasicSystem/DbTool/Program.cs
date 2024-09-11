using System;

namespace DbTool;

public static class Program {
    private static void Main(string[] args) {
        if (args.Length < 1)
            throw new ApplicationException("Need a param 'create' or 'drop'");

        string cmd = args.First().ToLowerInvariant();

        switch (cmd) {
            case "create":
                DbOperation.Create();
                Console.WriteLine("Tables created.");
                break;
            case "drop":
                ConfirmDrop();
                DbOperation.DropTables();
                Console.WriteLine("Tables dropped.");
                break;
            default:
                Console.WriteLine($"Unknown command: {cmd}");
                break;
        }
    }

    private static void ConfirmDrop() {
        const string failsafe = "yes drop em";
        Console.WriteLine();
        Console.WriteLine($"REALLY DROP ALL TABLES?  Type '{failsafe}' exactly to proceed:");
        Console.Write("> ");
        string? inp = Console.ReadLine();

        if (inp != failsafe)
            throw new ApplicationException("Failsafe string didn't match.  Aborting.");
    }
}
