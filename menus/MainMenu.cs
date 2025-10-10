namespace HealthClinic.menus;

using HealthClinic.utils;
public class MainMenu
{
    /// <summary>
    /// Displays the main menu and manages user navigation. Allows you to register customers, view customers, access queries, view pets, and log out of the system.
    /// The flow remains in a loop until the user decides to exit.
    /// </summary>
    public static void Menu()
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");

        while (true)
        {
            try
            {
                ConsoleUI.ShowMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerMenu.CustomerMainMenu();
                        continue;
                    case 2:
                        PetMenu.PetMainMenu();
                        continue;
                    case 3:
                        VeterinarianMenu.VeterinarianMainMenu();
                        continue;
                    case 4:
                        AppointmentMenu.AppointmentMainMenu();
                        continue;
                    case 5:
                        QuerysMenu.Querys();
                        break;
                    case 6:
                        Console.WriteLine("\n👋 Thanks for using HealthClinic System. Goodbye! 🐶🐱");
                        break; // Finish the program
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
                        continue;
                }
            }
            catch
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a number");
                continue;
            }
            break;
        }
    }
}
