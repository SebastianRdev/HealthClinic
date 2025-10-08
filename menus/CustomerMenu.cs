namespace HealthClinic.menus;

using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.utils;
using HealthClinic.repositories;
public class CustomerMenu
{
    static List<Customer> customerList = new Repository<Customer>().GetAll();
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
                        CustomerService.ViewCustomers(customerList);
                        continue;
                    case 3:
                        // Actualizar customer
                        continue;
                    case 4:
                        // Eliminar customer
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
