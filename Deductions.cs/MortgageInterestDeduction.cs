namespace TaxCalculatorProject.Deductions;

public class MortgageInterestDeduction : Deduction
{
    private const decimal MaxLimit = 150000;

    public MortgageInterestDeduction(decimal amount)
        : base(amount)
    {
        Category = Enums.DeductionCategory.MortgageInterest;
    }

    public override decimal CalculateAllowedDeduction()
    {
        return Math.Min(Amount, MaxLimit);
    }
}