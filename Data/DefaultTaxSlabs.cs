using TaxCalculatorProject.Models;

namespace TaxCalculatorProject.Data;

public static class DefaultTaxSlabs
{
    public static List<TaxSlab> GetSlabs()
    {
        return new List<TaxSlab>
        {
            new TaxSlab(0, 300000, 0.05m),
            new TaxSlab(300000, 600000, 0.10m),
            new TaxSlab(600000, 1000000, 0.15m),
            new TaxSlab(1000000, decimal.MaxValue, 0.20m)
        };
    }
}