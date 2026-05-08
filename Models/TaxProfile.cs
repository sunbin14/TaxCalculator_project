namespace TaxCalculatorProject.Models;

public class TaxProfile
{
    public decimal ExemptionLimit { get; set; }
    public decimal MaxMedicalDeduction { get; set; }
    public decimal MaxEducationDeduction { get; set; }
    public decimal MaxRebatePercentage { get; set; }

    public TaxProfile(decimal exemption, decimal medical, decimal education, decimal rebate)
    {
        ExemptionLimit = exemption;
        MaxMedicalDeduction = medical;
        MaxEducationDeduction = education;
        MaxRebatePercentage = rebate;
    }
}