namespace HealthClinic;

using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.utils;
using HealthClinic.repositories;

/// <summary>
/// Main class of the HealthClinic system. Orchestrates the application flow, initializes the main data, and manages user interaction through menus.
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point of the system. Displays the welcome message and calls up the main menu.
    /// </summary>
    public static void Main(string[] args)
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");
        menu();
    }

    /// Main lists obtained from the repository for use in the application.

    /// <summary>
    /// List of veterinarians used for veterinary-related operations.
    /// </summary>
    static List<Veterinarian> veterinarianList = new Repository<Veterinarian>().GetAll();

    /// <summary>
    /// List of customers used for customer-related operations.
    /// </summary>
    static List<Customer> customerList = new Repository<Customer>().GetAll();

    /// <summary>
    /// List of pets used for pet-related operations.
    /// </summary>
    static List<Pet> petList = new Repository<Pet>().GetAll();

    /// Dictionary obtained from the repository for use in the application.

    /// <summary>
    /// Client dictionary indexed by Guid, useful for quick searches and operations that require direct access by identifier.
    /// </summary>
    static Dictionary<Guid, Customer> customerDict = new RepositoryDict<Customer>().GetDictionary();


    /// <summary>
    /// Displays the main menu and manages user navigation. Allows you to register customers, view customers, access queries, view pets, and log out of the system.
    /// The flow remains in a loop until the user decides to exit.
    /// </summary>
    public static void menu()
    {

        while (true)
        {
            try
            {
                ConsoleUI.ShowMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerService.MainRegisterCustomer();
                        continue;
                    case 2:
                        CustomerService.ViewCustomers(customerList);
                        continue;
                    case 3:
                        QuerysMenu();
                        continue;
                    case 4:
                        PetService.ViewPets(petList);
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

    /// <summary>
    /// Displays the queries submenu and manages user navigation between different types of queries about customers and pets.
    /// Allows you to filter, group, sort, and perform combined queries, as well as return to the main menu.
    /// </summary>
    public static void QuerysMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowQueriesMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerService.FilterCustomersByPetAge(customerList);
                        continue;
                    case 2:
                        PetService.MainSort(petList);
                        continue;
                    case 3:
                        PetService.GroupPetsBySpecies(petList);
                        continue;
                    case 4:
                        PetService.CombinedConsultation(customerList);
                        continue;
                    case 5:
                        CustomerService.YoungestOrOlderCustomer(customerList);
                        continue;
                    case 6:
                        PetService.PetsOfEachSpecies(petList);
                        continue;
                    case 7:
                        CustomerService.CustomerUnknownPetBreed(customerList);
                        continue;
                    case 8:
                        CustomerService.CustomersInCapitalityAlphabetically(customerList);
                        continue;
                    case 9:
                        break; // Back the main menu
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
                        continue;
                }
            }
            catch
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a number, error: ");
                continue;
            }
            break;
        }
    }
}