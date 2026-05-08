using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public class IndividualTaxPayer : TaxPayer
{
    private int _age;
    private string _gender = string.Empty;

    public double MedicalExpenses { get; private set; }
    public double EducationalExpenses { get; private set; }
    public double MortgageInterest { get; private set; }

    public TaxRebate Rebate { get; private set; }
    public void SetRebate(TaxRebate rebate)
    {
        Rebate = rebate;
    }
    public double[] Taxtiers {get; private set; } = new double[6];

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0 || value > 120)
                throw new ArgumentException("Age must be between 0 and 120.");
            _age = value;
        }
    }
    public string Gender
    {
        get { return _gender; }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Gender cannot be null or empty.");
            _gender = value;
        }
    }

    public IndividualTaxPayer(string name, double annualIncome)
        : base(name, annualIncome)
    {
        PayerType = "I";
    }

    public override void Set_IndividualDetails(string gender, int age, double medical, double education, double mortgage)
    {
        Gender = gender;
        Age = age;
        if (Gender == "M")
            Deductions = 375000;
        if (Gender == "F" || Age >= 65)
            Deductions += 50000;
        MedicalExpenses = Math.Min(medical, 120000);
        EducationalExpenses = Math.Min(education, 80000);
        MortgageInterest = Math.Min(mortgage, 150000);
        Deductions += MedicalExpenses + EducationalExpenses + MortgageInterest;
    }

    public override void Set_CalculateTax()
    {
        double taxableIncome = GetTaxableIncome();
        if (taxableIncome <= 350000)
            {TaxAmount = 0.0;
            return;}

        if (taxableIncome > 450000)
            Taxtiers[0] = 5000;
        else
            {Taxtiers[0] = (taxableIncome - 350000) * 0.05;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 850000)
            Taxtiers[1] = 40000;
        else
            {Taxtiers[1] = (taxableIncome - 450000) * 0.1;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 1350000)
            Taxtiers[2] = 75000;
        else
            {Taxtiers[2] = (taxableIncome - 850000) * 0.15;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 1850000)
            Taxtiers[3] = 100000;
        else
            {Taxtiers[3] = (taxableIncome - 1350000) * 0.2;
            TaxAmount = Taxtiers.Sum();
            return;}
        if (taxableIncome > 3850000)
            Taxtiers[4] = 500000;
        else
            {Taxtiers[4] = (taxableIncome - 1850000) * 0.25;
            TaxAmount = Taxtiers.Sum();
            return;}
        Taxtiers[5] = (taxableIncome - 3850000) * 0.3;
        TaxAmount = Taxtiers.Sum();
        TaxAmount -= Rebate.TotalRebate;
        if (TaxAmount < 0)
            TaxAmount = 0;
    }
    public override void PrintTaxtierCalculation()
    {
        Console.WriteLine($"Tax Tier breakdown for {Name}:");
        Console.WriteLine($"Taxable Income: {GetTaxableIncome():C}");
        Console.WriteLine($"Tax for each tier:\n" +
                          $"Tier-1 (First 100,000): {Taxtiers[0]:C}\n" +
                          $"Tier-2 (Next 400,000): {Taxtiers[1]:C}\n" +
                          $"Tier-3 (Next 500,000): {Taxtiers[2]:C}\n" +
                          $"Tier-4 (Next 500,000): {Taxtiers[3]:C}\n" +
                          $"Tier-5 (Next 2,000,000): {Taxtiers[4]:C}\n" +
                          $"Tier-6 (Next remaining): {Taxtiers[5]:C}");
    }
}