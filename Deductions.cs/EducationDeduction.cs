namespace TaxCalculatorProject.Deductions;

public class EducationDeduction : Deduction
{
    private const decimal MaxLimit = 80000;

    public EducationDeduction(decimal amount)
        : base(amount)
    {
        Category = Enums.DeductionCategory.Education;
    }

    public override decimal CalculateAllowedDeduction()
    {
        return Math.Min(Amount, MaxLimit);
    }
}