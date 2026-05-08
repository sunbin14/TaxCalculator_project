namespace TaxCalculatorProject.Exceptions;

public class InvalidRebateException : Exception
{
    public InvalidRebateException()
        : base("Rebate value is invalid.")
    {
    }

    public InvalidRebateException(decimal amount)
        : base($"Invalid rebate value: {amount}. Must be non-negative.")
    {
    }
}