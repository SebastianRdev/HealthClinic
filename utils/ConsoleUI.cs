namespace HealthClinic.utils;

public class ConsoleUI
{
    public static void ShowMenu()
    {
        Console.WriteLine("\n📋 Main Menu:");
        Console.WriteLine("1️⃣  Register Customer");
        Console.WriteLine("2️⃣  View Customers");
        Console.WriteLine("3️⃣  Queries 🔍");
        Console.WriteLine("4️⃣  View pets 🐕🐈");
        Console.WriteLine("5️⃣  Exit 🚪");
    }

    public static void ShowQueriesMenu()
    {
        Console.WriteLine("\n🔍 Queries Menu:");
        Console.WriteLine("1️⃣  Filter Customers By Pet Age");
        Console.WriteLine("2️⃣  Sort by Pet(name, age, species)");
        Console.WriteLine("3️⃣  Group Pets by Species");
        Console.WriteLine("4️⃣  Combined consultation (Clients with a 3-year-old dog, providing their name and phone number)");
        Console.WriteLine("5️⃣  Find the customer(younger or older)");
        Console.WriteLine("6️⃣  How many pets are there of each species?");
        Console.WriteLine("7️⃣  Customer with a pet of undefined breed");
        Console.WriteLine("8️⃣  List customers alphabetically in uppercase letters.");
        Console.WriteLine("9️⃣  Back to Main Menu 🔙");
    }
}