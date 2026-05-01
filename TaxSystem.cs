using System;
using System.Linq;

public class TaxSystem
{
    private List<TaxRecord> _taxRecords = new List<TaxRecord>();

    public void AddRecord(TaxPayer taxPayer)
    {
        _taxRecords.Add(new TaxRecord(taxPayer));
    }

    public void PrintAllRecords()
    {
        if (_taxRecords.Count == 0)
        {
            Console.WriteLine("No records available.");
            return;
        }

        foreach (var record in _taxRecords)
        {
            Console.WriteLine(record.GetTaxBreakdown());
            Console.WriteLine(new string('-', 40));
        }
    }

    public void PrintPayersList()
    {
        if (_taxRecords.Count == 0)
        {
            Console.WriteLine("No taxpayers have been added yet.");
            return;
        }

        Console.WriteLine("List of Tax Payers:");
        foreach (var record in _taxRecords)
        {
            Console.WriteLine($"Record ID: {record.RecordID}, Name: {record.RecordedPayer.Name}, TIN: {record.RecordedPayer.TIN}");
        }
    }

    public void PrintRecordByID(string recordID)
    {
        if (string.IsNullOrWhiteSpace(recordID))
        {
            Console.WriteLine("Record ID cannot be empty.");
            return;
        }

        var record = _taxRecords.FirstOrDefault(r => string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));
        if (record != null)
            Console.WriteLine(record.GetTaxBreakdown());
        else
            Console.WriteLine("No record found for ID: " + recordID);
    }

    public void PrintTaxtierCalculationByID(string recordID)
    {
        if (string.IsNullOrWhiteSpace(recordID))
        {
            Console.WriteLine("Record ID cannot be empty.");
            return;
        }

        var record = _taxRecords.FirstOrDefault(r => string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));
        if (record != null)
            record.RecordedPayer.PrintTaxtierCalculation();
        else
            Console.WriteLine("No record found for ID: " + recordID);
    }

    public void DeleteRecordByID(string recordID)
    {
        if (string.IsNullOrWhiteSpace(recordID))
        {
            Console.WriteLine("Record ID cannot be empty.");
            return;
        }

        var record = _taxRecords.FirstOrDefault(r => string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));
        if (record != null)
        {
            _taxRecords.Remove(record);
            Console.WriteLine("Record with ID " + recordID + " has been deleted.");
        }
        else
        {
            Console.WriteLine("No record found for ID: " + recordID);
        }
    }

    public void ClearAllRecords()
    {
        _taxRecords.Clear();
        Console.WriteLine("All records have been cleared.");
    }

    public void UpdateRecordByID(string recordID)
    {
        if (string.IsNullOrWhiteSpace(recordID))
        {
            Console.WriteLine("Record ID cannot be empty.");
            return;
        }

        var record = _taxRecords.FirstOrDefault(r => string.Equals(r.RecordID, recordID.Trim(), StringComparison.OrdinalIgnoreCase));
        if (record == null)
        {
            Console.WriteLine("No record found for ID: " + recordID);
            return;
        }

        Console.WriteLine("Found---\n" +
                          $"Name: {record.RecordedPayer.Name}\n" +
                          $"Annual Income: {record.RecordedPayer.AnnualIncome:C}\n" +
                          $"Deductions: {record.RecordedPayer.Deductions:C}\n");

        bool updating = true;
        while (updating)
        {
            Console.Write("What do you want to update?\n" +
                          "1. Name\n" +
                          "2. Annual Income\n" +
                          "3. Deductions\n" +
                          "4. Exit\n\n" +
                          "Choose an option: ");
            var input = Console.ReadLine();

            try
            {
                switch (input)
                {
                    case "1":
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(newName))
                        {
                            Console.WriteLine("Name cannot be empty.");
                            break;
                        }

                        record.RecordedPayer.Name = newName;
                        Console.WriteLine("Name updated successfully.");
                        break;
                    case "2":
                        Console.Write("Enter new annual income: ");
                        if (!double.TryParse(Console.ReadLine(), out double newIncome))
                        {
                            Console.WriteLine("Invalid numeric value. Please try again.");
                            break;
                        }

                        record.RecordedPayer.UpdateIncome(newIncome);
                        record.UpdateTaxAmount();
                        Console.WriteLine("Annual income updated successfully.");
                        break;
                    case "3":
                        Console.Write("Enter new deductions: ");
                        if (!double.TryParse(Console.ReadLine(), out double newDeductions))
                        {
                            Console.WriteLine("Invalid numeric value. Please try again.");
                            break;
                        }

                        record.RecordedPayer.UpdateDeductions(newDeductions);
                        record.UpdateTaxAmount();
                        Console.WriteLine("Deductions updated successfully.");
                        break;
                    case "4":
                        updating = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("[REJECTED] " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred while updating the record: " + ex.Message);
            }
        }
    }
}