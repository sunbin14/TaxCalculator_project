using TaxCalculatorProject.Interfaces;

namespace TaxCalculatorProject.Rebates;

public class InsuranceRebate : InvestmentRebate
{
    private const decimal Rate = 0.10m;

    public InsuranceRebate(decimal amount)
        : base(amount)
    {
        Type = Enums.RebateType.Insurance;
    }

    public override decimal CalculateRebate()
    {
        return Amount * Rate;
    }
}