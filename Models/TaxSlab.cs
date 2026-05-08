namespace TaxCalculatorProject.Models;

public class TaxSlab
{
    public decimal MinIncome { get; set; }
    public decimal MaxIncome { get; set; }
    public decimal Rate { get; set; }

    public TaxSlab(decimal min, decimal max, decimal rate)
    {
        MinIncome = min;
        MaxIncome = max;
        Rate = rate;
    }

    public bool IsInSlab(decimal income)
    {
        return income > MinIncome && income <= MaxIncome;
    }
}