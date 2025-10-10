namespace HealthClinic;

using HealthClinic.models;
using HealthClinic.repositories;
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
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");
        MainMenu.Menu();
    }

    /// <summary>
    /// List of veterinarians used for veterinary-related operations.
    /// </summary>
    static List<Veterinarian> veterinarianList = new Repository<Veterinarian>().GetAll();
}