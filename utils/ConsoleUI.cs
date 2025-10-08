namespace HealthClinic.utils;

using HealthClinic.interfaces;

/// <summary>
/// Utility class for displaying menus in the HealthClinic system console.
/// Implements the IConsoleUI interface.
/// </summary>
public class ConsoleUI : IConsoleUI
{
    /// <summary>
    /// Displays the main menu of the application on the console.
    /// </summary>
    public static void ShowMenu()
    {
        Console.WriteLine("\n📋 Main Menu:");
        Console.WriteLine("1️⃣  Customers CRUD");
        Console.WriteLine("2️⃣  Pets CRUD 🐕🐈");
        Console.WriteLine("3️⃣  Queries 🔍");
        Console.WriteLine("5️⃣  Exit 🚪");
    }

    /// <summary>
    /// Displays the query menu on the console.
    /// </summary>
    public static void ShowQueriesMenu()
    {
        Console.WriteLine("\n🔍 Queries Menu:");
        Console.WriteLine("1️⃣  Filter Customers By Pet Age");
        Console.WriteLine("2️⃣  Sort by Pet(name, age, species)");
        Console.WriteLine("3️⃣  Group Pets by Species");
        Console.WriteLine("4️⃣  Combined consultation (Customers with a 3-year-old dog, providing their name and phone number)");
        Console.WriteLine("5️⃣  Find the customer(younger or older)");
        Console.WriteLine("6️⃣  How many pets are there of each species?");
        Console.WriteLine("7️⃣  Customer with a pet of undefined breed");
        Console.WriteLine("8️⃣  List customers alphabetically in uppercase letters.");
        Console.WriteLine("9️⃣  Back to Main Menu 🔙");
    }

    /// <summary>
    /// Displays the customerCRUD menu on the console.
    /// </summary>
    public static void ShowCustomerCRUD()
    {
        Console.WriteLine("\n📋 Customer CRUD:");
        Console.WriteLine("1️⃣  Register Customer");
        Console.WriteLine("2️⃣  View customers");
        Console.WriteLine("3️⃣  Update a customer");
        Console.WriteLine("4️⃣  Delete a customer");
        Console.WriteLine("5️⃣  Back to Main Menu 🔙");
    }

    /// <summary>
    /// Displays the petCRUD menu on the console.
    /// </summary>
    public static void ShowPetCRUD()
    {
        Console.WriteLine("\n📋 Pet CRUD:");
        Console.WriteLine("1️⃣  Register Pet");
        Console.WriteLine("2️⃣  View pets");
        Console.WriteLine("3️⃣  Update a pet");
        Console.WriteLine("4️⃣  Delete a pet");
        Console.WriteLine("5️⃣  Back to Main Menu 🔙");
    }
}