namespace TaxCalculatorProject.Utilities;

public static class ConsoleHelper
{
    public static void PrintHeader(string title)
    {
        Console.WriteLine("\n==============================");
        Console.WriteLine($" {title}");
        Console.WriteLine("==============================\n");
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERROR: {message}");
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"SUCCESS: {message}");
        Console.ResetColor();
    }
}