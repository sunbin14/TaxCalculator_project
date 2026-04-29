using System;

public class IndividualTaxPayer : TaxPayer
{
    public IndividualTaxPayer(string name, double annualIncome, double deductions)
        : base(name, annualIncome, deductions)
    {
    }

    public override double CalculateTax()
    {
        double taxableIncome = GetTaxableIncome();
        double tax = 0;
        if (taxableIncome <= 350000)
            return tax;
        if (taxableIncome <= 450000)
            tax += (taxableIncome - 350000) * 0.05;
        if (taxableIncome <= 850000)
            tax += (taxableIncome - 450000) * 0.1;
        if (taxableIncome <= 1350000)
            tax += (taxableIncome - 850000) * 0.15;
        if (taxableIncome <= 1850000)
            tax += (taxableIncome - 1350000) * 0.2;
        if (taxableIncome <= 3850000)
            tax += (taxableIncome - 1850000) * 0.25;
        if (taxableIncome > 3850000)
            tax += (taxableIncome - 3850000) * 0.3;
        return tax;
    }
}