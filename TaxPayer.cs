using System;

public abstract class TaxPayer
{
    private string _name = string.Empty;
    private string _tin = string.Empty;    
    private double _annualIncome;
    private double _deductions;
    public string PayerType { get; protected set; } = string.Empty;

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be null or empty.");
            _name = value;
        }
    }
    public double AnnualIncome
    {
        get { return _annualIncome; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Annual income cannot be negative.");
            _annualIncome = value;
        }
    }
    public double Deductions
    {
        get { return _deductions; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Deductions cannot be negative.");
            if (value > AnnualIncome)
                throw new ArgumentException("Deductions cannot exceed annual income.");
            if (value > 500000)
                throw new ArgumentException("Deductions cannot exceed 500,000.");
            _deductions = value;
        }
    }
    public string TIN
    {
        get { return _tin; }
        private set { _tin = value; }
    }

    public TaxPayer(string name, double annualIncome, double deductions)
    {
        Name = name;
        AnnualIncome = annualIncome;
        Deductions = deductions;
        TIN = Random.Shared.NextInt64(100_000_000_000, 999_999_999_999).ToString();
    }

    public abstract double CalculateTax();
    public abstract void PrintTaxtierCalculation();
    public double GetTaxableIncome()
    {
        return AnnualIncome - Deductions;
    }
    public void UpdateIncome(double newIncome)
    {
        if (newIncome < Deductions)
            throw new ArgumentException("Annual income cannot be less than existing deductions.");

        AnnualIncome = newIncome;
    }

    public void UpdateDeductions(double newDeductions)
    {
        Deductions = newDeductions;
    }
}