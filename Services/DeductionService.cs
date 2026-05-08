using TaxCalculatorProject.Deductions;

namespace TaxCalculatorProject.Services;

public class DeductionService
{
    public decimal CalculateTotalDeductions(List<Deduction> deductions)
    {
        return deductions.Sum(d => d.CalculateAllowedDeduction());
    }
    
    public Dictionary<string, decimal> GetBreakdown(List<Deduction> deductions)
    {
        var result = new Dictionary<string, decimal>();

        foreach (var d in deductions)
        {
            string key = d.Category.ToString();
            decimal value = d.CalculateAllowedDeduction();

            if (result.ContainsKey(key))
                result[key] += value;
            else
                result[key] = value;
        }

        return result;
    }
}