using TaxCalculatorProject.Exceptions;

namespace TaxCalculatorProject.Services;

public class ValidationService
{
    public void ValidateIncome(decimal income)
    {
        if (income < 0)
            throw new InvalidIncomeException();
    }

    public void ValidateDeductionAmount(decimal amount)
    {
        if (amount < 0)
            throw new InvalidDeductionException();
    }

    public void ValidateRebateAmount(decimal amount)
    {
        if (amount < 0)
            throw new InvalidRebateException();
    }
}
