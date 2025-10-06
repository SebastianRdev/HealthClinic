using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.utils;
using HealthClinic.repositories;
using HealthClinic.data;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");
        menu();

        
    }

    static List<Veterinarian> veterinarianList = new Repository<Veterinarian>().GetAll();
    static List<Customer> customerList = new Repository<Customer>().GetAll();
    static List<Pet> petList = new Repository<Pet>().GetAll();
    // static List<Customer> customersDict = new RepositoryDict<Customer>().GetAll();
    static Dictionary<Guid, Customer> customerDict = new RepositoryDict<Customer>().GetDictionary();


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
                        CustomerService.MainRegisterCustomer(customerList, customerDict, petList);
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
                        break;
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
            catch(Exception e)
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a number, error: ",e);
                continue;
            }
            break;
        }
    }
}