public class TaxRebate
{
    public double LifeInsurance { get; private set; }
    public double DPS { get; private set; }
    public double SavingsCertificate { get; private set; }
    public double TotalRebate { get; private set; }
    public TaxRebate(double lifeInsurance, double dps, double savingsCertificate)
    {
        LifeInsurance = Math.Min(lifeInsurance, 150000);
        DPS = Math.Min(dps, 150000);
        SavingsCertificate = Math.Min(savingsCertificate, 150000);
        TotalRebate = (LifeInsurance + DPS + SavingsCertificate) * 0.15;
    }
}