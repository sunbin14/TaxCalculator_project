namespace TaxCalculatorProject.Models;

public class TaxReport
{
    public decimal TotalIncome { get; set; }
    public decimal Exemption { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TaxableIncome { get; set; }

    public decimal TaxBeforeRebate { get; set; }
    public decimal TotalRebate { get; set; }
    public decimal FinalTax { get; set; }

    public DateTime GeneratedAt { get; set; }

    public TaxReport()
    {
        GeneratedAt = DateTime.Now;
    }
}