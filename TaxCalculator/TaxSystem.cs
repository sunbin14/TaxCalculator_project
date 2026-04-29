using System;
using System.Linq;
using System.Runtime.CompilerServices;

public class TaxSystem
{
    private List<TaxRecord> _taxRecords = new List<TaxRecord>();

    public void AddRecord(TaxPayer taxPayer)
    {
        _taxRecords.Add(new TaxRecord(taxPayer));
    }
    public void PrintAllRecords()
    {
        foreach (var record in _taxRecords)
        {
            Console.WriteLine(record.GetTaxBreakdown());
            Console.WriteLine(new string('-', 40));
        }
    }
    public void PrintRecordByTIN(string tin)
    {
        var record = _taxRecords.FirstOrDefault(r => r.RecordedPayer.TIN == tin);
        if (record != null)
            Console.WriteLine(record.GetTaxBreakdown());
        else
            Console.WriteLine("No record found for TIN: " + tin);
    }
    public void DeleteRecordByTIN(string tin)
    {
        var record = _taxRecords.FirstOrDefault(r => r.RecordedPayer.TIN == tin);
        if (record != null)
        {
            _taxRecords.Remove(record);
            Console.WriteLine("Record with TIN " + tin + " has been deleted.");
        }
        else
            Console.WriteLine("No record found for TIN: " + tin);
    }
}