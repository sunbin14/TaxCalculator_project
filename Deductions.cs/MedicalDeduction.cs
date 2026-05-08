namespace TaxCalculatorProject.Deductions;

public class MedicalDeduction : Deduction
{
    private const decimal MaxLimit = 120000;

    public MedicalDeduction(decimal amount)
        : base(amount)
    {
        Category = Enums.DeductionCategory.Medical;
    }

    public override decimal CalculateAllowedDeduction()
    {
        return Math.Min(Amount, MaxLimit);
    }
}