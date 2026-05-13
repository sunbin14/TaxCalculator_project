using System;
using System.Linq;

public class IndividualTaxPayer : TaxPayer
{
    private int _age;
    private string _gender = string.Empty;
    private TaxRebate? _rebate;
    private Deductions? _detailedDeductions;
    private readonly double[] _taxTiers = new double[6];

    public int Age
    {
        get => _age;
        private set
        {
            if (value < 0 || value > 120)
                throw new TaxPayerException("Age must be between 0 and 120.");
            _age = value;
        }
    }

    public string Gender
    {
        get => _gender;
        private set
        {
            if (value != "M" && value != "F")
                throw new TaxPayerException("Gender must be 'M' or 'F'.");
            _gender = value;
        }
    }

    public IndividualTaxPayer(string name, double annualIncome, string gender, int age)
        : base(name, annualIncome)
    {
        PayerType = "I";
        Gender = gender;
        Age = age;
    }

    public void SetRebate(TaxRebate rebate)
    {
        _rebate = rebate ?? throw new TaxPayerException("Rebate cannot be null.");
        CalculateTax();
    }

    public void SetDeductions(Deductions deductions)
    {
        _detailedDeductions = deductions ?? throw new TaxPayerException("Deductions cannot be null.");

        double exemption = TaxConstants.GeneralExemptionLimit;
        if (Gender == "F" || Age >= TaxConstants.SeniorAgeThreshold)
            exemption += TaxConstants.FemaleOrSeniorExemptionBonus;

        Deductions = exemption + _detailedDeductions.TotalDeductions;
        CalculateTax();
    }

    public string GetRebateDisplay()
        => _rebate?.ToString() ?? "N/A";

    public override string GetDeductionsDisplay()
        => _detailedDeductions?.ToString() ?? $"{Deductions:N0}";

    public override void CalculateTax()
    {
        Array.Clear(_taxTiers, 0, _taxTiers.Length);
        double income = GetTaxableIncome();

        if (income <= TaxConstants.TaxFreeLimit)
        {
            TaxAmount = 0;
            return;
        }

        double remaining = income - TaxConstants.TaxFreeLimit;

        double w1 = TaxConstants.Slab1Limit - TaxConstants.TaxFreeLimit;
        double w2 = TaxConstants.Slab2Limit - TaxConstants.Slab1Limit;
        double w3 = TaxConstants.Slab3Limit - TaxConstants.Slab2Limit;
        double w4 = TaxConstants.Slab4Limit - TaxConstants.Slab3Limit;
        double w5 = TaxConstants.Slab5Limit - TaxConstants.Slab4Limit;

        _taxTiers[0] = Math.Min(remaining, w1) * TaxConstants.Slab1Rate;
        remaining   -= w1;
        if (remaining <= 0) goto ApplyRebate;

        _taxTiers[1] = Math.Min(remaining, w2) * TaxConstants.Slab2Rate;
        remaining   -= w2;
        if (remaining <= 0) goto ApplyRebate;

        _taxTiers[2] = Math.Min(remaining, w3) * TaxConstants.Slab3Rate;
        remaining   -= w3;
        if (remaining <= 0) goto ApplyRebate;

        _taxTiers[3] = Math.Min(remaining, w4) * TaxConstants.Slab4Rate;
        remaining   -= w4;
        if (remaining <= 0) goto ApplyRebate;

        _taxTiers[4] = Math.Min(remaining, w5) * TaxConstants.Slab5Rate;
        remaining   -= w5;
        if (remaining > 0)
            _taxTiers[5] = remaining * TaxConstants.Slab6Rate;

        ApplyRebate:
        double grossTax = _taxTiers.Sum();
        TaxAmount = Math.Max(grossTax - (_rebate?.TotalRebate ?? 0), 0);
    }

    public override void PrintTaxTierBreakdown()
    {
        Console.WriteLine($"\n--- Tax Tier Breakdown: {Name} (Individual) ---");
        Console.WriteLine($"{"Taxable Income",-30}: {GetTaxableIncome(),15:N0} BDT");
        Console.WriteLine($"{"Tier 1 (350K–450K  @  5%)",-30}: {_taxTiers[0],15:N0} BDT");
        Console.WriteLine($"{"Tier 2 (450K–850K  @ 10%)",-30}: {_taxTiers[1],15:N0} BDT");
        Console.WriteLine($"{"Tier 3 (850K–1.35M @ 15%)",-30}: {_taxTiers[2],15:N0} BDT");
        Console.WriteLine($"{"Tier 4 (1.35M–1.85M@ 20%)",-30}: {_taxTiers[3],15:N0} BDT");
        Console.WriteLine($"{"Tier 5 (1.85M–3.85M@ 25%)",-30}: {_taxTiers[4],15:N0} BDT");
        Console.WriteLine($"{"Tier 6 (Above 3.85M@ 30%)",-30}: {_taxTiers[5],15:N0} BDT");
        Console.WriteLine($"{"Gross Tax",-30}: {_taxTiers.Sum(),15:N0} BDT");
        Console.WriteLine($"{"Investment Rebate",-30}: {(_rebate?.TotalRebate ?? 0),15:N0} BDT");
        Console.WriteLine($"{"Final Tax Due",-30}: {TaxAmount,15:N0} BDT");
    }
}
