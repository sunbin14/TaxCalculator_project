using TaxCalculatorProject.Rebates;

namespace TaxCalculatorProject.Services;

public class RebateService
{
    public decimal CalculateTotalRebate(List<InvestmentRebate> rebates)
    {
        decimal total = 0;

        foreach (var rebate in rebates)
        {
            total += rebate.CalculateRebate();
        }

        return total;
    }

    public Dictionary<string, decimal> GetBreakdown(List<InvestmentRebate> rebates)
    {
        var result = new Dictionary<string, decimal>();

        foreach (var rebate in rebates)
        {
            string key = rebate.Type.ToString();
            decimal value = rebate.CalculateRebate();

            if (result.ContainsKey(key))
                result[key] += value;
            else
                result[key] = value;
        }

        return result;
    }
}