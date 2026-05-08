using TaxCalculatorProject.Models;
using TaxCalculatorProject.Enums;

namespace TaxCalculatorProject.Core;

public class TaxRuleEngine
{
    public decimal GetExemptionLimit(TaxPayer taxPayer)
    {
        return taxPayer.Type switch
        {
            TaxPayerType.Individual when taxPayer.Gender == Gender.Female => 425000,
            TaxPayerType.Individual => 375000,
            TaxPayerType.Business => 0,
            _ => 375000
        };
    }
}