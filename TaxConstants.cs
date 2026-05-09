/// <summary>
/// Centralised constants for the Bangladesh Tax System.
/// Change values here to update the entire system.
/// </summary>
public static class TaxConstants
{
    // ── Expense deduction caps (BDT) ──────────────────────────────────────────
    public const double MaxMedicalExpenses          = 120_000;
    public const double MaxEducationalExpenses      =  80_000;
    public const double MaxMortgageInterest         = 150_000;
    public const double MaxTotalDeductions          = 870_000;

    // ── Basic exemption limits (BDT) ──────────────────────────────────────────
    public const double GeneralExemptionLimit           = 375_000;
    public const double FemaleOrSeniorExemptionBonus    =  50_000;

    // ── Investment rebate ─────────────────────────────────────────────────────
    public const double MaxRebatePerCategory        = 150_000;
    public const double RebateRate                  =    0.15;   // 15%

    // ── Tax rates ─────────────────────────────────────────────────────────────
    public const double BusinessFlatTaxRate         =    0.25;   // 25%

    // ── Thresholds ────────────────────────────────────────────────────────────
    public const int    SeniorAgeThreshold          =      65;

    // ── Individual tax slab boundaries (BDT) ─────────────────────────────────
    public const double TaxFreeLimit   = 350_000;
    public const double Slab1Limit     = 450_000;
    public const double Slab2Limit     = 850_000;
    public const double Slab3Limit   = 1_350_000;
    public const double Slab4Limit   = 1_850_000;
    public const double Slab5Limit   = 3_850_000;

    // ── Slab tax amounts (BDT) ────────────────────────────────────────────────
    public const double Slab1MaxTax   =   5_000;
    public const double Slab2MaxTax   =  40_000;
    public const double Slab3MaxTax   =  75_000;
    public const double Slab4MaxTax   = 100_000;
    public const double Slab5MaxTax   = 500_000;

    // ── Slab rates ────────────────────────────────────────────────────────────
    public const double Slab1Rate     = 0.05;
    public const double Slab2Rate     = 0.10;
    public const double Slab3Rate     = 0.15;
    public const double Slab4Rate     = 0.20;
    public const double Slab5Rate     = 0.25;
    public const double Slab6Rate     = 0.30;
}
