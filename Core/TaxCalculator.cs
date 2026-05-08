using TaxCalculatorProject.Models;
using TaxCalculatorProject.Services;

namespace TaxCalculatorProject.Core;

public class TaxCalculator
{
    private readonly DeductionService _deductionService;
    private readonly RebateService _rebateService;
    private readonly SlabService _slabService;
    private readonly ValidationService _validationService;

    public TaxCalculator()
    {
        _deductionService = new DeductionService();
        _rebateService = new RebateService();
        _slabService = new SlabService();
        _validationService = new ValidationService();
    }

    public TaxReport Calculate(TaxPayer taxPayer)
    {
        _validationService.ValidateIncome(taxPayer.AnnualIncome);

        decimal exemption = 0; // later from rule engine or profile

        decimal totalDeductions =
            _deductionService.CalculateTotalDeductions(taxPayer.Deductions);

        decimal taxableIncome =
            taxPayer.AnnualIncome - exemption - totalDeductions;

        decimal taxBeforeRebate =
            _slabService.CalculateTax(taxableIncome, GetDefaultSlabs());

        decimal totalRebate =
            _rebateService.CalculateTotalRebate(taxPayer.Rebates);

        decimal finalTax = taxBeforeRebate - totalRebate;

        return new TaxReport
        {
            TotalIncome = taxPayer.AnnualIncome,
            Exemption = exemption,
            TotalDeductions = totalDeductions,
            TaxableIncome = taxableIncome,
            TaxBeforeRebate = taxBeforeRebate,
            TotalRebate = totalRebate,
            FinalTax = finalTax
        };
    }

    private List<TaxSlab> GetDefaultSlabs()
    {
        return new List<TaxSlab>
        {
            new TaxSlab(0, 300000, 0.05m),
            new TaxSlab(300000, 600000, 0.10m),
            new TaxSlab(600000, decimal.MaxValue, 0.15m)
        };
    }
}