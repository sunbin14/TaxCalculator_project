public class Deductions
{
    public double MedicalExpenses { get; private set; }
    public double EducationalExpenses { get; private set; }
    public double Mortgage { get; private set; }
    public double TotalDeductions { get; private set; }

    public Deductions(double medicalExpenses, double educationalExpenses, double mortgage)
    {
        if (medicalExpenses < 0) throw new TaxPayerException("Medical expenses cannot be negative.");
        if (educationalExpenses < 0) throw new TaxPayerException("Educational expenses cannot be negative.");
        if (mortgage < 0) throw new TaxPayerException("Home loan interest cannot be negative.");

        MedicalExpenses = Math.Min(medicalExpenses, TaxConstants.MaxMedicalExpenses);
        EducationalExpenses = Math.Min(educationalExpenses, TaxConstants.MaxEducationalExpenses);
        Mortgage = Math.Min(mortgage, TaxConstants.MaxMortgageInterest);

        TotalDeductions = MedicalExpenses + EducationalExpenses + Mortgage;
    }

    public override string ToString()
        => $"Home Loan: {Mortgage:N0} | Education: {EducationalExpenses:N0} | " +
           $"Medical: {MedicalExpenses:N0} => Total: {TotalDeductions:N0}";
}
