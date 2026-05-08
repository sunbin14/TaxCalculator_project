namespace TaxCalculatorProject.Exceptions;

public class InvalidDeductionException : Exception
{
    public InvalidDeductionException()
        : base("Deduction amount is invalid.")
    {
    }

    public InvalidDeductionException(decimal amount)
        : base($"Invalid deduction value: {amount}. Must be non-negative.")
    {
    }
}