using TaxCalculatorProject.Models;
using TaxCalculatorProject.Services;

namespace TaxCalculatorProject.Core;

public class TaxSystem
{
    private readonly TaxCalculator _calculator;
    private readonly InputService _input;

    public TaxSystem()
    {
        _calculator = new TaxCalculator();
        _input = new InputService();
    }

    public void Start()
    {
        Console.WriteLine("=== TAX CALCULATOR SYSTEM ===");

        var taxPayer = new TaxPayer();

        taxPayer.Name = Console.ReadLine();

        taxPayer.AnnualIncome = _input.GetDecimalInput("Enter Income: ");

        var report = _calculator.Calculate(taxPayer);

        Console.WriteLine("Calculation Completed...");
    }
}