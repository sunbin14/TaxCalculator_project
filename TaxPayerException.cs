using System;

public class TaxPayerException : Exception
{
    public TaxPayerException(string message) : base(message) { }
    public TaxPayerException(string message, Exception inner) : base(message, inner) { }
}
