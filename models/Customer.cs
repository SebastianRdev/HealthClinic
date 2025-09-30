namespace HealthClinic.models;

using HealthClinic.utils;

public class Customer
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Ages { get; set; }
    public List<Pet> Pets { get; set; } = new List<Pet>();

    // public Customer(string name, int ages)
    // {
    //     Id = Guid.NewGuid();
    //     Name = name;
    //     Ages = ages;
    // }

    public static void MainRegisterCustomer()
    {
        Console.WriteLine("\n--- 📝 Register Customer ---");
        RegisterCustomerMenu();
        Console.WriteLine("\n✅ Customer registered successfully");
    }

    public static void RegisterCustomerMenu()
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
        RegisterCustomer(name, ages);
    }

    public static void RegisterCustomer(string name, int ages)
    {
        Customer newCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = name,
            Ages = ages
        };
    }
}
