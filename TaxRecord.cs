using System;

public class TaxRecord
{
    private static int _counter = 101;

    public string RecordID { get; private set; }
    public TaxPayer RecordedPayer { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public TaxRecord(TaxPayer taxPayer)
    {
        RecordedPayer = taxPayer ?? throw new TaxPayerException("TaxPayer cannot be null.");
        RecordID = $"{taxPayer.PayerType}-{_counter++}";
        CreatedAt = DateTime.Now;
        RecordedPayer.CalculateTax();
    }

    public string GetTaxBreakdown()
    {
        string payerTypeName = RecordedPayer is IndividualTaxPayer ? "Individual" : "Business";

        string rebateInfo = RecordedPayer is IndividualTaxPayer individual
            ? individual.GetRebateDisplay()
            : "N/A";

        string deductionsInfo = RecordedPayer.GetDeductionsDisplay();

        return
            $"{"Record ID",-20}: {RecordID}\n" +
            $"{"Created At",-20}: {CreatedAt:yyyy-MM-dd HH:mm}\n" +
            $"{"Payer Type",-20}: {payerTypeName}\n" +
            $"{"Name",-20}: {RecordedPayer.Name}\n" +
            $"{"TIN",-20}: {RecordedPayer.TIN}\n" +
            $"{"Annual Income",-20}: {RecordedPayer.AnnualIncome:N0} BDT\n" +
            $"{"Deductions",-20}: {deductionsInfo} BDT\n" +
            $"{"Taxable Income",-20}: {RecordedPayer.GetTaxableIncome():N0} BDT\n" +
            $"{"Rebate",-20}: {rebateInfo}\n" +
            $"{"Tax Due",-20}: {RecordedPayer.TaxAmount:N0} BDT";
    }
}
