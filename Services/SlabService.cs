using TaxCalculatorProject.Models;

namespace TaxCalculatorProject.Services;

public class SlabService
{
    public decimal CalculateTax(decimal taxableIncome, List<TaxSlab> slabs)
    {
        decimal tax = 0;

        foreach (var slab in slabs)
        {
            if (taxableIncome > slab.MinIncome)
            {
                decimal upperLimit = slab.MaxIncome;
                decimal applicableIncome = Math.Min(taxableIncome, upperLimit) - slab.MinIncome;

                if (applicableIncome > 0)
                {
                    tax += applicableIncome * slab.Rate;
                }
            }
        }

        return tax;
    }
}