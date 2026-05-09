// ── Entry Point ───────────────────────────────────────────────────────────────
// All UI logic is in ConsoleUI; all business logic is in TaxSystem.
// Program.cs is intentionally minimal.

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
        // Domain errors (validation failures, business rules)
        Console.WriteLine($"\n[Tax Error] {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n[REJECTED] {ex.Message}");
    }
    catch (Exception ex)
    {
        // Unexpected / programming errors
        Console.WriteLine($"\n[Unexpected Error] {ex.Message}");
    }
}
