namespace TaxCalculatorProject.Rebates;

public class DPSRebate : InvestmentRebate
{
    private const decimal Rate = 0.15m;

    public DPSRebate(decimal amount)
        : base(amount)
    {
        Type = Enums.RebateType.DPS;
    }

    public override decimal CalculateRebate()
    {
        return Amount * Rate;
    }
}