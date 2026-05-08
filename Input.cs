using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Input
{
    public bool Status { get; private set; } = true;
    TaxSystem taxSystem = new TaxSystem();
    public void ShowMenu()
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

        string choice = GetString("Enter your choice: ");
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
                taxSystem.PrintRecordByID(GetString("Enter Record ID: "));
                break;
            case "5":
                taxSystem.PrintPayersList();
                taxSystem.PrintTaxtierCalculationByID(GetString("Enter Record ID: "));
                break;
            case "6":
                taxSystem.PrintPayersList();
                taxSystem.DeleteRecordByID(GetString("Enter Record ID to delete: "));
                break;
            case "7":
                taxSystem.PrintPayersList();
                taxSystem.UpdateRecordByID(GetString("Enter Record ID to update: "));
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

    public void AddTaxRecord()
    {
        string name = GetString("Enter Name: ");
        double income = GetDouble("Enter Annual Income: ");
        string type = GetString("Is this an Individual (I) or Business (B) taxpayer? ").ToUpper();
        TaxPayer taxPayer = type switch
        {
            "I" => new IndividualTaxPayer(name, income),
            "B" => new BusinessTaxPayer(name, income),
            _ => throw new ArgumentException("Invalid taxpayer type. Enter 'I' or 'B'.")
        };
        double deductions;
        if (type == "I")
        {
            string gender = GetString("Enter Gender (M or F): ").ToUpper();
            int age = GetInt("Enter Age: ");
            double medical = GetDouble("Enter Medical Expenses: ");
            double education = GetDouble("Enter Educational Expenses: ");
            double mortgage = GetDouble("Enter Mortgage Interest: ");
            deductions = medical + education + mortgage;
            taxPayer.Set_IndividualDetails(gender, age, medical, education, mortgage);
            double lifeInsurance = GetDouble("Enter Life Insurance Premium: ");
            double dps = GetDouble("Enter DPS Amount: ");
            double savingsCertificate = GetDouble("Enter Savings Certificate Amount: ");
            TaxRebate rebate = new TaxRebate(lifeInsurance, dps, savingsCertificate);
            ((IndividualTaxPayer)taxPayer).SetRebate(rebate);
        }
        else if (type == "B")
        {
            deductions = GetDouble("Enter Deductions: ");
            taxPayer.UpdateDeductions(deductions);
        }

        taxSystem.AddRecord(taxPayer);
        Console.WriteLine("Record added successfully.");
    }

    public string GetString(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(input))
            return input;

        throw new ArgumentException("Input cannot be empty. Please try again.");
    }

    public double GetDouble(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (!double.TryParse(input, out double value))
            throw new ArgumentException("Invalid number format.");

        return value;
    }
    public int GetInt(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int value))
            throw new ArgumentException("Invalid integer format.");

        return value;
    }
}