using TaxCalculatorProject.Enums;

namespace TaxCalculatorProject.Rebates;

public abstract class InvestmentRebate
{
    public RebateType Type { get; protected set; }
    public decimal Amount { get; set; }

    protected InvestmentRebate(decimal amount)
    {
        Amount = amount;
    }

    public abstract decimal CalculateRebate();
}