using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages the collection of TaxRecord objects.
/// Provides full CRUD operations plus reporting and statistics.
///
/// Single Responsibility: this class only manages records.
/// All console I/O for user input is handled in ConsoleUI.
/// </summary>
public class TaxSystem
{
    private readonly List<TaxRecord> _records = new();

    // ── CRUD ──────────────────────────────────────────────────────────────────

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

    // ── Print / reporting ─────────────────────────────────────────────────────

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
        if (record is null) return;
        Console.WriteLine("\n--- Tax Record ---");
        Console.WriteLine(record.GetTaxBreakdown());
    }

    public void PrintTaxTierBreakdownByID(string recordID)
    {
        var record = FindRecord(recordID);
        if (record is null) return;
        record.RecordedPayer.PrintTaxTierBreakdown();
    }

    /// <summary>
    /// Prints system-wide statistics:
    /// total tax collected, average, individual vs business count, and top payer.
    /// </summary>
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

    // ── Update (interactive — driven by ConsoleUI) ────────────────────────────

    public TaxRecord UpdateRecordByID(string recordID)
    {
        return FindRecord(recordID);
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    /// <summary>
    /// Finds a record by ID (case-insensitive). Prints a message and returns null if not found.
    /// </summary>
    private TaxRecord? FindRecord(string recordID)
    {
        if (string.IsNullOrWhiteSpace(recordID))
        {
            throw new ArgumentException("Record ID cannot be empty.");
        }

        var record = _records.FirstOrDefault(r =>
            string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));

        if (record is null)
            throw new ArgumentException($"No record found for ID: {recordID}");

        return record;
    }

    /// <summary>Returns true and prints a message when there are no records.</summary>
    private bool NoRecords()
    {
        if (_records.Count > 0) return false;
        Console.WriteLine("No records available.");
        return true;
    }
}
