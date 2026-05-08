namespace TaxCalculatorProject.Rebates;

public class SavingsCertificateRebate : InvestmentRebate
{
    private const decimal Rate = 0.08m;

    public SavingsCertificateRebate(decimal amount)
        : base(amount)
    {
        Type = Enums.RebateType.SavingsCertificate;
    }

    public override decimal CalculateRebate()
    {
        return Amount * Rate;
    }
}