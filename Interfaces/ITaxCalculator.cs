using TaxCalculatorProject.Models;

namespace TaxCalculatorProject.Interfaces;

public interface ITaxCalculator
{
    TaxReport CalculateTax(TaxPayer taxPayer);
}