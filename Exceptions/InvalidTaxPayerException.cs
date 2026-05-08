namespace TaxCalculatorProject.Exceptions;

public class InvalidTaxPayerException : Exception
{
    public InvalidTaxPayerException()
        : base("Taxpayer data is invalid.")
    {
    }

    public InvalidTaxPayerException(string message)
        : base(message)
    {
    }
}