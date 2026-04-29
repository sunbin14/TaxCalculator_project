using System;
using System.Collections.Generic;
using System.Linq;

TaxSystem taxSystem = new TaxSystem();
Console.WriteLine("=-=-=-=- Welcome to the Tax Calculator! -=-=-=-=\n" +
                    "Please select from Menu:");
while (true)
{
    Console.WriteLine("=========================================\n" + 
                        "1. Add Tax Record\n" +
                        "2. View All Records\n" +
                        "3. View Record by TIN\n" +
                        "4. Delete Record by TIN\n" +
                        "5. Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine() ?? string.Empty;
    Console.WriteLine(new string('-', 40));
    switch (choice)
    {
        case "1":
            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Annual Income: ");
            double income = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter Deductions: ");
            double deductions = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Is this an Individual (I) or Business (B) taxpayer? ");
            string type = (Console.ReadLine() ?? string.Empty).ToUpper();
            TaxPayer taxPayer;
            if (type == "I")
                taxPayer = new IndividualTaxPayer(name, income, deductions);
            else
                taxPayer = new BusinessTaxPayer(name, income, deductions);
            taxSystem.AddRecord(taxPayer);
            break;
        case "2":
            taxSystem.PrintAllRecords();
            break;
        case "3":
            Console.Write("Enter TIN to search: ");
            string searchTIN = Console.ReadLine() ?? string.Empty;
            taxSystem.PrintRecordByTIN(searchTIN);
            break;
        case "4":
            Console.Write("Enter TIN to delete: ");
            string deleteTIN = Console.ReadLine() ?? string.Empty;
            taxSystem.DeleteRecordByTIN(deleteTIN);
            break;
        case "5":
            Console.WriteLine("Exiting... Thank you for using the Tax Calculator!");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            Console.WriteLine(new string('-', 40));
            break;
    }
}
