public class Program
{
    public static void Main(string[] args)
    {
        show_menu();
        menu();
    }

    public static void show_menu()
    {
        Console.WriteLine("1. Register Customer");
        Console.WriteLine("2. View Customers");
        Console.WriteLine("3. Search Customers");
        Console.WriteLine("4. Exit");
    }

    public static void menu()
    {
        while (true)
        {
            try {
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice){
                    case 1:
                        RegisterCustomer();
                        break;
                    case 2:
                        ViewCustomers();
                        break;
                    case 3:
                        SearchCustomers();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        show_menu();
                        menu();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}