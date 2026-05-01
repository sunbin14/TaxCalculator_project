using System;

public class BusinessTaxPayer : TaxPayer
{
    public BusinessTaxPayer(string name, double annualIncome, double deductions)
        : base(name, annualIncome, deductions)
    {
        PayerType = "B";
    }

    public override double CalculateTax()
    {
        return GetTaxableIncome() * 0.25;
    }
    public override void PrintTaxtierCalculation()
    {
        Console.WriteLine($"Tax Tier calculation for {Name}:");
        Console.WriteLine($"Taxable Income: {GetTaxableIncome():C}");
        Console.WriteLine($"Calculated Tax (flat 25% of taxable income): {GetTaxableIncome() * 0.25:C}");
    }
}