using System;

public class TaxRebate
{
    public double LifeInsurance { get; private set; }
    public double DPS { get; private set; }
    public double SavingsCertificate { get; private set; }
    public double TotalRebate { get; private set; }

    public TaxRebate(double lifeInsurance, double dps, double savingsCertificate)
    {
        if (lifeInsurance < 0) throw new TaxPayerException("Life insurance amount cannot be negative.");
        if (dps < 0) throw new TaxPayerException("DPS amount cannot be negative.");
        if (savingsCertificate < 0) throw new TaxPayerException("Savings certificate amount cannot be negative.");

        LifeInsurance = Math.Min(lifeInsurance, TaxConstants.MaxRebatePerCategory);
        DPS = Math.Min(dps, TaxConstants.MaxRebatePerCategory);
        SavingsCertificate = Math.Min(savingsCertificate, TaxConstants.MaxRebatePerCategory);

        TotalRebate = (LifeInsurance + DPS + SavingsCertificate) * TaxConstants.RebateRate;
    }

    public override string ToString()
        => $"Life Ins: {LifeInsurance:N0} | DPS: {DPS:N0} | Savings Cert: {SavingsCertificate:N0} " +
           $"=> Rebate: {TotalRebate:N0} BDT";
}
