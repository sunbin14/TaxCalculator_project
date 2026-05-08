using TaxCalculatorProject.Enums;

namespace TaxCalculatorProject.Deductions;

public abstract class Deduction
{
    public DeductionCategory Category { get; protected set; }
    public decimal Amount { get; set; }

    protected Deduction(decimal amount)
    {
        Amount = amount;
    }

    public abstract decimal CalculateAllowedDeduction();
}