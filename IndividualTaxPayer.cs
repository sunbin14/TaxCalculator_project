using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public class IndividualTaxPayer : TaxPayer
{
    public double[] Taxtiers {get; private set; } = new double[6];
    public IndividualTaxPayer(string name, double annualIncome, double deductions)
        : base(name, annualIncome, deductions)
    {
        PayerType = "I";
    }

    public override void Set_CalculateTax()
    {
        double taxableIncome = GetTaxableIncome();
        if (taxableIncome <= 350000)
            {TaxAmount = 0.0;
            return;}

        if (taxableIncome > 450000)
            Taxtiers[0] = 5000;
        else
            {Taxtiers[0] = (taxableIncome - 350000) * 0.05;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 850000)
            Taxtiers[1] = 40000;
        else
            {Taxtiers[1] = (taxableIncome - 450000) * 0.1;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 1350000)
            Taxtiers[2] = 75000;
        else
            {Taxtiers[2] = (taxableIncome - 850000) * 0.15;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 1850000)
            Taxtiers[3] = 100000;
        else
            {Taxtiers[3] = (taxableIncome - 1350000) * 0.2;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 3850000)
            Taxtiers[4] = 500000;
        else
            {Taxtiers[4] = (taxableIncome - 1850000) * 0.25;
            TaxAmount = Taxtiers.Sum();
            return;}
        Taxtiers[5] = (taxableIncome - 3850000) * 0.3;
        TaxAmount = Taxtiers.Sum();
    }
    public override void PrintTaxtierCalculation()
    {
        Console.WriteLine($"Tax Tier breakdown for {Name}:");
        Console.WriteLine($"Taxable Income: {GetTaxableIncome():C}");
        Console.WriteLine($"Tax for each tier:\n" +
                          $"Zero Tier (0 - 350,000): {0:C}\n" +
                          $"First Tier (350,001 - 450,000): {Taxtiers[0]:C}\n" +
                          $"Second Tier (450,001 - 850,000): {Taxtiers[1]:C}\n" +
                          $"Third Tier (850,001 - 1,350,000): {Taxtiers[2]:C}\n" +
                          $"Fourth Tier (1,350,001 - 1,850,000): {Taxtiers[3]:C}\n" +
                          $"Fifth Tier (1,850,000): {Taxtiers[4]:C}\n" +
                          $"Sixth Tier (Above 3,850,000): {Taxtiers[5]:C}");
    }
}