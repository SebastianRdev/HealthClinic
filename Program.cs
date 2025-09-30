using HealthClinic.models;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");
        menu();
    }

    public static void show_menu()
    {
        Console.WriteLine("\n📋 Main Menu:");
        Console.WriteLine("1️⃣  Register Customer");
        Console.WriteLine("2️⃣  View Customers");
        Console.WriteLine("3️⃣  Search Customers");
        Console.WriteLine("4️⃣  Exit 🚪");
    }

    public static void menu()
    {
        while (true)
        {
            try
            {
                show_menu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Customer.MainRegisterCustomer();
                        break;
                    case 2:
                        // ViewCustomers();
                        break;
                    case 3:
                        // SearchCustomers();
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