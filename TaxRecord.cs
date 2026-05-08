using System;
using System.Data.Common;

public class TaxRecord
{
    private static int _counter = 101;
    public string RecordID { get; private set; }
    public TaxPayer RecordedPayer { get; private set; }
    // public double TaxAmount { get; private set; }

    public TaxRecord(TaxPayer taxPayer)
    {
        RecordedPayer = taxPayer;
        RecordID = taxPayer.PayerType + "-" + _counter++;
        RecordedPayer.Set_CalculateTax();

    }

    public string GetTaxBreakdown()
    {
        return $"Tax Record ID: {RecordID}\n" +
               $"Name: {RecordedPayer.Name}\n" +
               $"TIN: {RecordedPayer.TIN}\n" +
               $"Annual Income: {RecordedPayer.AnnualIncome:C}\n" +
               $"Deductions: {RecordedPayer.Deductions:C}\n" +
               $"Taxable Income: {RecordedPayer.GetTaxableIncome():C}\n" +
               $"Rebate: {(RecordedPayer is IndividualTaxPayer individual ? individual.Rebate.TotalRebate.ToString("C") : "N/A")}\n" +
               $"Calculated Tax: {RecordedPayer.TaxAmount:C}";
    }
    
}