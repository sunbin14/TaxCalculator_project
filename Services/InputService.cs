namespace TaxCalculatorProject.Services;

public class InputService
{
    public decimal GetDecimalInput(string prompt)
    {
        decimal value;

        while (true)
        {
            Console.Write(prompt);

            if (decimal.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public int GetIntInput(string prompt)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Invalid input. Try again.");
        }
    }
}