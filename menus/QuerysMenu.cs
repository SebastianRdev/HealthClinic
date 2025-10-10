namespace HealthClinic.menus;

using HealthClinic.models;
using HealthClinic.repositories;
using HealthClinic.services;
using HealthClinic.utils;
public class QuerysMenu
{
    /// <summary>
    /// List of customers used for customer-related operations.
    /// </summary>
    static List<Customer> customerList = new Repository<Customer>().GetAll();

    /// <summary>
    /// List of pets used for pet-related operations.
    /// </summary>
    static List<Pet> petList = new Repository<Pet>().GetAll();

    /// <summary>
    /// Displays the queries submenu and manages user navigation between different types of queries about customers and pets.
    /// Allows you to filter, group, sort, and perform combined queries, as well as return to the main menu.
    /// </summary>
    public static void Querys()
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
