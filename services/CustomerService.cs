namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;

public class CustomerService
{
    public static void MainRegisterCustomer(List<Customer> customers)
    {
        Console.WriteLine("\n--- 📝 Register Customer ---");

        Customer newCustomer = RegisterCustomerMenu();

        RegisterCustomer(customers, newCustomer);

        Console.WriteLine("\n✅ Customer registered successfully");
    }

    public static Customer RegisterCustomerMenu()
    {
        string name;
        int ages;

        while (true)
        {
            Console.Write("\n👤 Enter customer name: ");
            name = Console.ReadLine()!;
            if (!Validator.IsEmpty(name)) continue;
            break;
        }

        while (true)
        {
            try
            {
                Console.Write("\n🎂 Enter customer ages: ");
                ages = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(ages)) continue;
                break;
            }
            catch
            {
                Console.WriteLine("❌ Invalid input. Please enter a number");
                continue;
            }
        }

        return new Customer(name, ages);
    }

    public static void RegisterCustomer(List<Customer> customers, Customer newCustomer)
    {
        customers.Add(newCustomer);
    }

    public static void ViewCustomers(List<Customer> customers)
    {
        Console.WriteLine("\n--- 👥 Customer List ---");
        if (customers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found.");
            return;
        }
        foreach (var customer in customers)
        {
            Console.WriteLine($"\n🆔 ID: {customer.Id}");
            Console.WriteLine($"👤 Name: {customer.Name}");
            Console.WriteLine($"🎂 Ages: {customer.Ages}");
            Console.WriteLine($"🐾 Pets Count: {customer.Pets.Count}");
        }
    }

    public static void SearchCustomerByName(List<Customer> customers, string name)
    {
        Console.Write("\n🔍 Enter customer name to search: ");
        string searchName = Console.ReadLine()!;
        var foundCustomers = customers.Where(c => c.Name!.Equals(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
        if (foundCustomers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found with that name.");
            return;
        }
        ViewCustomers(foundCustomers);
    }
}