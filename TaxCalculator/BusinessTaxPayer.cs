using System;

public class BusinessTaxPayer : TaxPayer
{
    public BusinessTaxPayer(string name, double annualIncome, double deductions)
        : base(name, annualIncome, deductions)
    {
        
    }

    public override double CalculateTax()
    {
        return GetTaxableIncome() * 0.25;
    }
}