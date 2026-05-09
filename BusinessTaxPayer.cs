using System;

/// <summary>
/// Represents a business entity tax payer.
/// Applies a flat 25% tax rate on taxable income
/// (annual income minus allowable business deductions).
/// </summary>
public class BusinessTaxPayer : TaxPayer
{
    // ── Constructor ───────────────────────────────────────────────────────────
    public BusinessTaxPayer(string name, double annualIncome)
        : base(name, annualIncome)
    {
        PayerType = "B";
    }

    // ── Setup ─────────────────────────────────────────────────────────────────

    /// <summary>
    /// Sets allowable business deductions (e.g. operating costs, depreciation).
    /// Validates input before delegating to the base-class property.
    /// </summary>
    public void SetBusinessDeductions(double deductions)
    {
        if (deductions < 0)
            throw new TaxPayerException("Business deductions cannot be negative.");
        Deductions = deductions;   // base-class setter applies caps automatically
    } 

    // ── Tax calculation ───────────────────────────────────────────────────────

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
