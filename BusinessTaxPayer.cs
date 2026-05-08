using System;

public class BusinessTaxPayer : TaxPayer
{
    public BusinessTaxPayer(string name, double annualIncome, double deductions)
        : base(name, annualIncome, deductions)
    {
        PayerType = "B";
    }

    public override void Set_CalculateTax()
    {
        TaxAmount = GetTaxableIncome() * 0.25;
    }
    public override void PrintTaxtierCalculation()
    {
        Console.WriteLine(  $"Tax Tier breakdown for {Name}:\n" +
                            $"Taxable Income: {GetTaxableIncome():C}\n" +
                            $"Calculated Tax (flat 25% of taxable income): {TaxAmount:C}");
    }
}