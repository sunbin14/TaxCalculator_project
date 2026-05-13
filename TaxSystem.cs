using System;
using System.Collections.Generic;
using System.Linq;

public class TaxSystem
{
    private readonly List<TaxRecord> _records = new();

    public void AddRecord(TaxPayer taxPayer)
    {
        _records.Add(new TaxRecord(taxPayer));
        Console.WriteLine("Record added successfully.");
    }

    public void DeleteRecordByID(string recordID)
    {
        var record = FindRecord(recordID);
        if (record is null) return;
        _records.Remove(record);
        Console.WriteLine($"Record {recordID} deleted successfully.");
    }

    public void ClearAllRecords()
    {
        _records.Clear();
        Console.WriteLine("All records cleared.");
    }

    public void PrintPayersList()
    {
        if (NoRecords()) return;
        Console.WriteLine("\n--- Tax Payers List ---");
        Console.WriteLine($"  {"ID",-8} | {"Name",-28} | TIN");
        Console.WriteLine(new string('-', 55));
        foreach (var r in _records)
            Console.WriteLine($"  {r.RecordID,-8} | {r.RecordedPayer.Name,-28} | {r.RecordedPayer.TIN}");
        Console.WriteLine(new string('-', 55));
    }

    public void PrintAllRecords()
    {
        if (NoRecords()) return;
        Console.WriteLine("\n======= All Tax Records =======");
        foreach (var r in _records)
        {
            Console.WriteLine(r.GetTaxBreakdown());
            Console.WriteLine(new string('-', 45));
        }
    }

    public void PrintRecordByID(string recordID)
    {
        var record = FindRecord(recordID);
        Console.WriteLine("\n--- Tax Record ---");
        Console.WriteLine(record.GetTaxBreakdown());
    }

    public void PrintTaxTierBreakdownByID(string recordID)
    {
        var record = FindRecord(recordID);
        if (record is null) return;
        record.RecordedPayer.PrintTaxTierBreakdown();
    }

    public void PrintStatistics()
    {
        if (NoRecords()) return;

        double total    = _records.Sum(r => r.RecordedPayer.TaxAmount);
        double average  = total / _records.Count;
        int    indCount = _records.Count(r => r.RecordedPayer is IndividualTaxPayer);
        int    bizCount = _records.Count - indCount;
        var    topPayer = _records.OrderByDescending(r => r.RecordedPayer.TaxAmount).First();
        var    lowPayer = _records.OrderBy(r => r.RecordedPayer.TaxAmount).First();

        Console.WriteLine("\n======= Tax System Statistics =======");
        Console.WriteLine($"{"Total Records",-28}: {_records.Count}");
        Console.WriteLine($"{"  Individual Payers",-28}: {indCount}");
        Console.WriteLine($"{"  Business Payers",-28}: {bizCount}");
        Console.WriteLine($"{"Total Tax Collected",-28}: {total:N0} BDT");
        Console.WriteLine($"{"Average Tax Per Payer",-28}: {average:N0} BDT");
        Console.WriteLine($"{"Highest Tax Payer",-28}: {topPayer.RecordedPayer.Name} " +
                          $"({topPayer.RecordID}) — {topPayer.RecordedPayer.TaxAmount:N0} BDT");
        Console.WriteLine($"{"Lowest Tax Payer",-28}: {lowPayer.RecordedPayer.Name} " +
                          $"({lowPayer.RecordID}) — {lowPayer.RecordedPayer.TaxAmount:N0} BDT");
        Console.WriteLine(new string('=', 45));
    }

    public TaxRecord UpdateRecordByID(string recordID)
    {
        return FindRecord(recordID);
    }

    
    private TaxRecord? FindRecord(string recordID)
    {
        var record = _records.FirstOrDefault(r =>
            string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));

        if (record is null)
            throw new ArgumentException($"No record found for ID: {recordID}");

        return record;
    }

    private bool NoRecords()
    {
        if (_records.Count > 0) return false;
        Console.WriteLine("No records available.");
        return true;
    }
}
