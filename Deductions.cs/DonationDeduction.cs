namespace TaxCalculatorProject.Deductions;

public class DonationDeduction : Deduction
{
    public DonationDeduction(decimal amount)
        : base(amount)
    {
        Category = Enums.DeductionCategory.Donation;
    }

    public override decimal CalculateAllowedDeduction()
    {
        // Assume donation fully allowed (simplified rule)
        return Amount;
    }
}