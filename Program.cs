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

    public static List<Customer> Customers = new List<Customer>();

    public static void menu()
    {
        
        while (true)
        {
            try
            {
                ConsoleUI.show_menu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerService.MainRegisterCustomer(Customers);
                        break;
                    case 2:
                        // CustomerService.ViewCustomers(Customers);
                        break;
                    case 3:
                        // CustomerService.SearchCustomerByName(Customers, Console.ReadLine()!);
                        break;
                    case 4:
                        Console.WriteLine("\n👋 Thanks for using HealthClinic System. Goodbye! 🐶🐱");
                        break;
                    default:
                        Console.WriteLine("\n⚠️ Invalid choice. Please try again");
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