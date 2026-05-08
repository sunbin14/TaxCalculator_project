namespace TaxCalculatorProject.Utilities;

public static class TaxConstants
{
    public const decimal DEFAULT_MEDICAL_LIMIT = 120000;
    public const decimal DEFAULT_EDUCATION_LIMIT = 80000;
    public const decimal DEFAULT_MORTGAGE_LIMIT = 150000;

    public const decimal DEFAULT_DPS_RATE = 0.15m;
    public const decimal DEFAULT_INSURANCE_RATE = 0.10m;
    public const decimal DEFAULT_RETIREMENT_RATE = 0.12m;

    public const decimal GENERAL_EXEMPTION = 375000;
    public const decimal FEMALE_EXEMPTION = 425000;
    public const decimal DISABLED_EXEMPTION = 500000;
}