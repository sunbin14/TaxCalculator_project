using System;

public class BusinessTaxPayer : TaxPayer
{
    public BusinessTaxPayer(string name, double annualIncome)
        : base(name, annualIncome)
    {
        PayerType = "B";
    }

    public void SetBusinessDeductions(double deductions)
    {
        if (deductions < 0)
            throw new TaxPayerException("Business deductions cannot be negative.");
        Deductions = deductions;
    } 

    
    public override void CalculateTax()
    {
        TaxAmount = GetTaxableIncome() * TaxConstants.BusinessFlatTaxRate;
    }

    public override void PrintTaxTierBreakdown()
    {
        Console.WriteLine($"\n--- Tax Breakdown: {Name} (Business) ---");
        Console.WriteLine($"{"Annual Income",-25}: {AnnualIncome,15:N0} BDT");
        Console.WriteLine($"{"Deductions",-25}: {Deductions,15:N0} BDT");
        Console.WriteLine($"{"Taxable Income",-25}: {GetTaxableIncome(),15:N0} BDT");
        Console.WriteLine($"{"Tax Rate",-25}: {TaxConstants.BusinessFlatTaxRate,14:P0}");
        Console.WriteLine($"{"Tax Amount Due",-25}: {TaxAmount,15:N0} BDT");
    }
}
