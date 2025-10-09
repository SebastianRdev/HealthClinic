namespace HealthClinic.menus;

using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.utils;
using HealthClinic.repositories;
public class CustomerMenu
{
    /// <summary>
    /// Client dictionary indexed by Guid, useful for quick searches and operations that require direct access by identifier.
    /// </summary>
    static RepositoryDict<Customer> customerDictRep = new RepositoryDict<Customer>();

    public static void CustomerCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowCustomerCRUD();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerService.MainRegisterCustomer();
                        continue;
                    case 2:
                        CustomerService.ViewCustomers(customerDictRep.GetAll());
                        continue;
                    case 3:
                        CustomerService.UpdateCustomerMenu(customerDictRep);
                        continue;
                    case 4:
                        CustomerService.RemoveCustomer(customerDictRep);
                        continue;
                    case 5:
                        Console.WriteLine("\nBack the main menu 🐶🐱");
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
}
