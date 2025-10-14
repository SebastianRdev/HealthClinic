namespace HealthClinic.menus;

using HealthClinic.services;
using HealthClinic.utils;
public class CustomerMenu
{
    public static void CustomerMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowCustomerMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerCRUD();
                        continue;
                    case 2:
                        Console.WriteLine("\nBack to main menu");
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
                        CustomerService.ViewCustomers();
                        continue;
                    case 3:
                        CustomerService.UpdateCustomer();
                        continue;
                    case 4:
                        CustomerService.RemoveCustomer();
                        continue;
                    case 5:
                        Console.WriteLine("\nBack to Customer main menu");
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
