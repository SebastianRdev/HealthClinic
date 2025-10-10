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
                        QuerysMenu.Querys();
                        continue;
                    case 4:

                        continue;
                    case 5:
                        Console.WriteLine("\n👋 Thanks for using HealthClinic System. Goodbye! 🐶🐱");
                        break; // Back the main menu
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
