using System;

/// <summary>
/// Thrown when a tax-payer-related business rule is violated.
/// Use this instead of generic ArgumentException for domain errors.
/// </summary>
public class TaxPayerException : Exception
{
    public TaxPayerException(string message) : base(message) { }
    public TaxPayerException(string message, Exception inner) : base(message, inner) { }
}
