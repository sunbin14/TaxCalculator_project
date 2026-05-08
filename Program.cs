using TaxCalculatorProject.Core;

namespace TaxCalculatorProject;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            TaxSystem system = new TaxSystem();
            system.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error occurred.");
            Console.WriteLine(ex.Message);
        }
    }
}