namespace TaxCalculatorProject.Data;

public static class TaxConfigurations
{
    // Exemption limits
    public const decimal GeneralExemption = 375000;
    public const decimal FemaleExemption = 425000;
    public const decimal DisabledExemption = 500000;

    // Deduction limits
    public const decimal MedicalDeductionLimit = 120000;
    public const decimal EducationDeductionLimit = 80000;
    public const decimal MortgageDeductionLimit = 150000;

    // Rebate rates
    public const decimal DPSRate = 0.15m;
    public const decimal InsuranceRate = 0.10m;
    public const decimal RetirementRate = 0.12m;
    public const decimal SavingsCertificateRate = 0.08m;
}