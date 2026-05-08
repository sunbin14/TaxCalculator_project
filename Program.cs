using System;
using System.Collections.Generic;
using System.Linq;

Input input = new Input();
TaxSystem taxSystem = new TaxSystem();
Console.WriteLine("=-=-=-=- Welcome to the Tax Calculator! -=-=-=-=\n" +
                    "Please select from Menu:");
while (input.Status)
{
    try
    {
        input.ShowMenu();
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
