using TaxCalculatorProject.Enums;
using TaxCalculatorProject.Deductions;
using TaxCalculatorProject.Rebates;
namespace TaxCalculatorProject.Models;

public class TaxPayer
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public TaxPayerType Type { get; set; }
    public SpecialTaxStatus SpecialStatus { get; set; }
    public decimal AnnualIncome { get; set; }

    public List<Deduction> Deductions { get; set; }
    public List<InvestmentRebate> Rebates { get; set; }

    public TaxPayer()
    {
        Deductions = new List<Deduction>();
        Rebates = new List<InvestmentRebate>();
        SpecialStatus = SpecialTaxStatus.None;
    }

    public TaxPayer(string name, int age, Gender gender, TaxPayerType type, decimal income)
    {
        Name = name;
        Age = age;
        Gender = gender;
        Type = type;
        AnnualIncome = income;

        Deductions = new List<Deduction>();
        Rebates = new List<InvestmentRebate>();
        SpecialStatus = SpecialTaxStatus.None;
    }
}