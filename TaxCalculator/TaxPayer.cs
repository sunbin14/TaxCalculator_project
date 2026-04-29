using System;

public abstract class TaxPayer
{
    private string _name;
    private string _tin;    
    private double _annualIncome;
    private double _deductions;

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
            if (value > AnnualIncome || value > GetTaxableIncome() || value > 500000)
                throw new ArgumentException("Deductions cannot exceed annual income or taxable income or 500000.");
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
    public double GetTaxableIncome()
    {
        return AnnualIncome - Deductions;
    }
}