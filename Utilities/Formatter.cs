namespace TaxCalculatorProject.Utilities;

public static class Formatter
{
    public static string ToCurrency(decimal amount)
    {
        return $"{amount:N2} BDT";
    }

    public static string ToPercentage(decimal value)
    {
        return $"{value * 100:0.##}%";
    }

    public static string FormatLine(string label, decimal value)
    {
        return $"{label}: {ToCurrency(value)}";
    }
}