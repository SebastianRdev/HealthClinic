namespace HealthClinic;

using HealthClinic.menus;

/// <summary>
/// Main class of the HealthClinic system. Orchestrates the application flow, initializes the main data, and manages user interaction through menus.
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point of the system. Displays the welcome message and calls up the main menu.
    /// </summary>
    public static void Main()
    {
        MainMenu.Menu();
    }
}