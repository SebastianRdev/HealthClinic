using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.utils;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");
        menu();
    }

    public static List<Customer> CustomerList = new List<Customer>();
    public static Dictionary<Guid, Customer> CustomerDict = new Dictionary<Guid, Customer>();
    public static List<Pet> pets = new List<Pet>();

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
                        CustomerService.MainRegisterCustomer(CustomerList, CustomerDict, pets);
                        continue;
                    case 2:
                        CustomerService.ViewCustomers(CustomerList);
                        continue;
                    case 3:
                        QuerysMenu();
                        continue;
                    case 4:
                        PetService.ViewPets(pets);
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
                        CustomerService.FilterCustomersByPetAge(CustomerList);
                        continue;
                    case 2:
                        PetService.MainSort(pets);
                        continue;
                    case 3:
                        PetService.GroupPetsBySpecies(pets);
                        continue;
                    case 4:
                        PetService.CombinedConsultation(CustomerList);
                        continue;
                    case 5:
                        CustomerService.YoungestOrOlderCustomer(CustomerList);
                        continue;
                    case 6:
                        PetService.PetsOfEachSpecies(pets);
                        continue;
                    case 7:
                        CustomerService.CustomerUnknownPetBreed(CustomerList);
                        continue;
                    case 8:
                        CustomerService.CustomersInCapitalityAlphabetically(CustomerList);
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