using System;

public abstract class TaxPayer
{
    private string _name = string.Empty;
    private string _tin = string.Empty;
    private double _annualIncome;
    private double _deductions;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new TaxPayerException("Name cannot be null or empty.");
            _name = value.Trim();
        }
    }

    public string TIN
    {
        get => _tin;
        private set => _tin = value;
    }

    public string PayerType { get; protected set; } = string.Empty;

    public double AnnualIncome
    {
        get => _annualIncome;
        private set
        {
            if (value < 0)
                throw new TaxPayerException("Annual income cannot be negative.");
            _annualIncome = value;
        }
    }

    public double Deductions
    {
        get => _deductions;
        protected set
        {
            _deductions = Math.Min(value, TaxConstants.MaxTotalDeductions);
            _deductions = Math.Min(_deductions, AnnualIncome);
        }
    }

    public double TaxAmount { get; protected set; }

    protected TaxPayer(string name, double annualIncome)
    {
        Name = name;
        AnnualIncome = annualIncome;
        TIN = Random.Shared.NextInt64(100_000_000_000L, 999_999_999_999L).ToString();
        Deductions = 0;
    }

    public double GetTaxableIncome() => AnnualIncome - Deductions;

    public void UpdateIncome(double newIncome)
    {
        AnnualIncome = newIncome;
        Deductions = Deductions;
        CalculateTax();
    }

    public virtual void UpdateDeductions(double newDeductions)
    {
        Deductions = newDeductions;
        CalculateTax();
    }

    public virtual string GetDeductionsDisplay() => $"{Deductions:N0}";

    public abstract void CalculateTax();
    public abstract void PrintTaxTierBreakdown();
}
