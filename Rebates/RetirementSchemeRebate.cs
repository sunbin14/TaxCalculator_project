namespace TaxCalculatorProject.Rebates;

public class RetirementSchemeRebate : InvestmentRebate
{
    private const decimal Rate = 0.12m;

    public RetirementSchemeRebate(decimal amount)
        : base(amount)
    {
        Type = Enums.RebateType.RetirementScheme;
    }

    public override decimal CalculateRebate()
    {
        return Amount * Rate;
    }
}