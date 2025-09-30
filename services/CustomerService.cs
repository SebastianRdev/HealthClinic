namespace HealthClinic.services;

using HealthClinic.models;

public class CustomerService
{
    private static List<Customer> Customers = new List<Customer>();
    public static void RegisterCustomer(string name, int ages)
    {
        Customer newCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = name,
            Ages = ages
        };

        Customers.Add(newCustomer);
    }

    public static void ViewCustomers(List<Customer> customers)
    {
        Console.WriteLine("\n--- 👥 Customer List ---");
        if (customers.Count == 0)
        {
            Console.WriteLine("⚠️ No customers found.");
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

    public static void SearchCustomerByName()
    {
        Console.Write("\n🔍 Enter customer name to search: ");
        string searchName = Console.ReadLine()!;
        var foundCustomers = Customers.Where(c => c.Name!.Equals(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
        if (foundCustomers.Count == 0)
        {
            Console.WriteLine("⚠️ No customers found with that name.");
            return;
        }
        ViewCustomers(foundCustomers);
    }
}