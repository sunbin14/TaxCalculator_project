namespace TaxCalculatorProject.Exceptions;

public class InvalidIncomeException : Exception
{
    public InvalidIncomeException()
        : base("Income cannot be negative or zero.")
    {
    }

    public InvalidIncomeException(decimal income)
        : base($"Invalid income value: {income}. Income must be greater than 0.")
    {
    }
}