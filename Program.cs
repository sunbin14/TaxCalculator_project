using System;
using System.Collections.Generic;
using System.Linq;

TaxSystem taxSystem = new TaxSystem();
Console.WriteLine("=-=-=-=- Welcome to the Tax Calculator! -=-=-=-=\n" +
                    "Please select from Menu:");
bool Status = true;
while (Status)
{
    try
    {
        ShowMenu();
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("[REJECTED] " + ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error occurred: " + ex.Message);
    }
}

void ShowMenu()
{
    Console.WriteLine("=========================================" +
                      "\n1. Add Tax Record" +
                      "\n2. View All Records" +
                      "\n3. View Payers List" +
                      "\n4. View Record by ID" +
                      "\n5. View Tax Tier Calculation by ID" +
                      "\n6. Delete Record by ID" +
                      "\n7. Update Record by ID" +
                      "\n8. Clear All Records" +
                      "\n9. Exit");

    string choice = ReadRequiredString("Enter your choice: ");
    Console.WriteLine(new string('-', 40));

    switch (choice)
    {
        case "1":
            AddTaxRecord();
            break;
        case "2":
            taxSystem.PrintAllRecords();
            break;
        case "3":
            taxSystem.PrintPayersList();
            break;
        case "4":
            taxSystem.PrintPayersList();
            taxSystem.PrintRecordByID(ReadRequiredString("Enter Record ID: "));
            break;
        case "5":
            taxSystem.PrintPayersList();
            taxSystem.PrintTaxtierCalculationByID(ReadRequiredString("Enter Record ID: "));
            break;
        case "6":
            taxSystem.PrintPayersList();
            taxSystem.DeleteRecordByID(ReadRequiredString("Enter Record ID to delete: "));
            break;
        case "7":
            taxSystem.PrintPayersList();
            taxSystem.UpdateRecordByID(ReadRequiredString("Enter Record ID to update: "));
            break;
        case "8":
            taxSystem.ClearAllRecords();
            break;
        case "9":
            Console.WriteLine("Exiting... Thank you for using the Tax Calculator!");
            Status = false;
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void AddTaxRecord()
{
    string name = ReadRequiredString("Enter Name: ");
    double income = ReadDouble("Enter Annual Income: ", minValue: 0);
    double deductions = ReadDouble("Enter Deductions: ", minValue: 0);
    string type = ReadRequiredString("Is this an Individual (I) or Business (B) taxpayer? ").ToUpper();

    TaxPayer taxPayer = type switch
    {
        "I" => new IndividualTaxPayer(name, income, deductions),
        "B" => new BusinessTaxPayer(name, income, deductions),
        _ => throw new ArgumentException("Invalid taxpayer type. Enter 'I' or 'B'.")
    };

    taxSystem.AddRecord(taxPayer);
    Console.WriteLine("Record added successfully.");
}

string ReadRequiredString(string prompt)
{
    Console.Write(prompt);
    string? input = Console.ReadLine()?.Trim();

    if (!string.IsNullOrEmpty(input))
        return input;

    throw new ArgumentException("Input cannot be empty. Please try again.");
}

double ReadDouble(string prompt, double minValue = double.MinValue, double maxValue = double.MaxValue)
{
    Console.Write(prompt);
    string? input = Console.ReadLine();

    if (!double.TryParse(input, out double value))
        throw new ArgumentException("Invalid number format.");

    if (value < minValue)
        throw new ArgumentException($"Value must be at least {minValue}.");

    if (value > maxValue)
        throw new ArgumentException($"Value cannot exceed {maxValue}.");

    return value;
}

