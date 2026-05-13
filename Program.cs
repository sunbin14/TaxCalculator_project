Console.WriteLine("=-=-=-=- Welcome to the Tax Calculator! -=-=-=-=");

ConsoleUI ui = new ConsoleUI();

while (ui.IsRunning)
{
    try
    {
        ui.ShowMenu();
    }
    catch (TaxPayerException ex)
    {
        Console.WriteLine($"\n[Tax Error] {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n[REJECTED] {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[Unexpected Error] {ex.Message}");
    }
}
