using TaxCalculatorProject.Models;

namespace TaxCalculatorProject.Core;

public class ReportGenerator
{
    public void PrintReport(TaxReport report)
    {
        Console.WriteLine("\n===== TAX REPORT =====");

        Console.WriteLine($"Income: {report.TotalIncome}");
        Console.WriteLine($"Exemption: {report.Exemption}");
        Console.WriteLine($"Deductions: {report.TotalDeductions}");
        Console.WriteLine($"Taxable Income: {report.TaxableIncome}");
        Console.WriteLine($"Tax Before Rebate: {report.TaxBeforeRebate}");
        Console.WriteLine($"Rebate: {report.TotalRebate}");
        Console.WriteLine($"Final Tax: {report.FinalTax}");

        Console.WriteLine("======================");
    }
}