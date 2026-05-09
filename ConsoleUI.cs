using System;

/// <summary>
/// Handles all console input/output for the Tax Calculator application.
///
/// OOP improvement (Single Responsibility Principle):
///   This class is responsible ONLY for user interaction.
///   All business logic lives in TaxSystem and the TaxPayer hierarchy.
///
/// Key improvement over the original Input class:
///   Input helpers use retry loops instead of throwing exceptions on bad input.
///   One typo no longer cancels the entire operation — the user is simply
///   prompted again.
/// </summary>
public class ConsoleUI
{
    private readonly TaxSystem _taxSystem = new();
    public bool IsRunning { get; private set; } = true;

    // ── Menu ──────────────────────────────────────────────────────────────────

    public void ShowMenu()
    {
        Console.WriteLine(
            "\n=========================================" +
            "\n 1. Add Tax Record"                        +
            "\n 2. View All Records"                      +
            "\n 3. View Payers List"                      +
            "\n 4. View Record by ID"                     +
            "\n 5. View Tax Tier Breakdown by ID"         +
            "\n 6. Delete Record by ID"                   +
            "\n 7. Update Record by ID"                   +
            "\n 8. Statistics"                            +
            "\n 9. Clear All Records"                     +
            "\n 0. Exit"                                  +
            "\n=========================================");

        string choice = PromptString("Enter your choice: ");
        Console.WriteLine(new string('-', 42));

        switch (choice)
        {
            case "1":
                AddTaxRecord();
                break;
            case "2":
                _taxSystem.PrintAllRecords();
                break;
            case "3":
                _taxSystem.PrintPayersList();
                break;
            case "4":
                _taxSystem.PrintPayersList();
                _taxSystem.PrintRecordByID(PromptString("Enter Record ID: "));
                break;
            case "5":
                _taxSystem.PrintPayersList();
                _taxSystem.PrintTaxTierBreakdownByID(PromptString("Enter Record ID: "));
                break;
            case "6":
                _taxSystem.PrintPayersList();
                _taxSystem.DeleteRecordByID(PromptString("Enter Record ID to delete: "));
                break;
            case "7":
                _taxSystem.PrintPayersList();
                UpdateRecord(_taxSystem.UpdateRecordByID(PromptString("Enter Record ID to update: ")));
                break;
            case "8":
                _taxSystem.PrintStatistics();
                break;
            case "9":
                _taxSystem.ClearAllRecords();
                break;
            case "0":
                Console.WriteLine("Exiting... Thank you for using the Tax Calculator!");
                IsRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Please select 0–9.");
                break;
        }
    }

    // ── Add record flow ───────────────────────────────────────────────────────

    private void AddTaxRecord()
    {
        string name   = PromptString("Enter Name: ");
        double income = PromptNonNegativeDouble("Enter Annual Income (BDT): ");
        string type   = PromptChoice("Taxpayer type — Individual (I) or Business (B): ", "I", "B");

        TaxPayer taxPayer;

        if (type == "I")
        {
            string gender    = PromptChoice("Gender (M / F): ", "M", "F");
            int    age       = PromptNonNegativeInt("Age: ");
            var payer = new IndividualTaxPayer(name, income, gender, age);
            SetupIndividualPayer(payer);
            taxPayer = payer;
        }
        else
        {
            var payer = new BusinessTaxPayer(name, income);
            SetupBusinessPayer(payer);
            taxPayer = payer;
        }

        _taxSystem.AddRecord(taxPayer);
    }

    private void SetupIndividualPayer(IndividualTaxPayer payer)
    {
        UpdateIndividualDeductions(payer);
        UpdateIndividualRebates(payer);        
    }
    private void UpdateIndividualDeductions(IndividualTaxPayer payer)
    {
        Console.WriteLine("\n-- Expense Deductions --");
        double medical   = PromptNonNegativeDouble($"Medical Expenses       (max {TaxConstants.MaxMedicalExpenses:N0} BDT): ");
        double education = PromptNonNegativeDouble($"Educational Expenses   (max {TaxConstants.MaxEducationalExpenses:N0} BDT): ");
        double mortgage  = PromptNonNegativeDouble($"Mortgage Interest      (max {TaxConstants.MaxMortgageInterest:N0} BDT): ");

        payer.SetDeductions(new Deductions(medical, education, mortgage));
    }
    private void UpdateIndividualRebates(IndividualTaxPayer payer)
    {
        Console.WriteLine("\n-- Investment Rebate (15% credit on eligible amounts) --");
        double lifeIns = PromptNonNegativeDouble($"Life Insurance Premium (max {TaxConstants.MaxRebatePerCategory:N0} BDT): ");
        double dps     = PromptNonNegativeDouble($"DPS / Retirement       (max {TaxConstants.MaxRebatePerCategory:N0} BDT): ");
        double savCert = PromptNonNegativeDouble($"Savings Certificate    (max {TaxConstants.MaxRebatePerCategory:N0} BDT): ");

        payer.SetRebate(new TaxRebate(lifeIns, dps, savCert));
    }

    private void SetupBusinessPayer(BusinessTaxPayer payer)
    {
        double deductions = PromptNonNegativeDouble("Allowable Business Deductions (BDT): ");
        payer.SetBusinessDeductions(deductions);
    }
    
    private void UpdateRecord(TaxRecord taxRecord)
    {
        Console.WriteLine(
            $"\n--- Updating: {taxRecord.RecordedPayer.Name} ({taxRecord.RecordID}) ---\n" +
            $"Current Income     : {taxRecord.RecordedPayer.AnnualIncome:N0} BDT\n" +
            $"Current Deductions : {taxRecord.RecordedPayer.Deductions:N0} BDT");
        bool updating = true;
        while (updating)
        {
            Console.Write(
                "\nWhat to update?\n" +
                "  1. Name\n"         +
                "  2. Annual Income\n" +
                "  3. Deductions\n"    +
                "  4. Tax Rebates\n"   +
                "  5. Back\n"         +
                "Choice: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    
                    taxRecord.RecordedPayer.Name = PromptString("New Name: ");
                    Console.WriteLine("Name updated.");
                    break;

                case "2":
                    taxRecord.RecordedPayer.UpdateIncome(PromptNonNegativeDouble("New annual income (BDT): "));
                    Console.WriteLine($"Income updated. New tax: {taxRecord.RecordedPayer.TaxAmount:N0} BDT");
                    break;

                case "3":
                    if (taxRecord.RecordedPayer.PayerType == "I")
                    {
                        UpdateIndividualDeductions((IndividualTaxPayer)taxRecord.RecordedPayer);
                        Console.WriteLine($"Deductions updated. New tax: {taxRecord.RecordedPayer.TaxAmount:N0} BDT");
                    }
                    else
                    {
                        taxRecord.RecordedPayer.UpdateDeductions(PromptNonNegativeDouble("New deductions (BDT): "));
                        Console.WriteLine($"Deductions updated. New tax: {taxRecord.RecordedPayer.TaxAmount:N0} BDT");
                    }
                    break;

                case "4":
                    if (taxRecord.RecordedPayer.PayerType == "I")
                    {
                        UpdateIndividualRebates((IndividualTaxPayer)taxRecord.RecordedPayer);
                        Console.WriteLine($"Rebates updated. New tax: {taxRecord.RecordedPayer.TaxAmount:N0} BDT");
                    }
                    else Console.WriteLine($"Tax Rebate is not applicable for {taxRecord.RecordedPayer.GetType().Name}");
                    break;
                case "5":
                    updating = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please enter 1–5.");
                    break;
            }
        }
    }

    // ── Input helpers (all retry on bad input — no exceptions thrown to caller) ─

    /// <summary>Prompts until a non-empty string is entered.</summary>
    public string PromptString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(input))
                return input;
            Console.WriteLine("  [!] Input cannot be empty. Please try again.");
        }
    }

    /// <summary>Prompts until one of the allowed values is entered (case-insensitive).</summary>
    public string PromptChoice(string prompt, params string[] allowed)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().ToUpper();
            if (input is not null && Array.Exists(allowed, a => a == input))
                return input;
            Console.WriteLine($"  [!] Please enter one of: {string.Join(" / ", allowed)}");
        }
    }

    /// <summary>Prompts until a non-negative double is entered.</summary>
    public double PromptNonNegativeDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double value) && value >= 0)
                return value;
            Console.WriteLine("  [!] Please enter a valid non-negative number.");
        }
    }

    /// <summary>Prompts until a non-negative integer is entered.</summary>
    public int PromptNonNegativeInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                return value;
            Console.WriteLine("  [!] Please enter a valid non-negative integer.");
        }
    }
}
