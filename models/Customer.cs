namespace HealthClinic.models;

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
        RegisterCustomerMenu();
        Console.WriteLine("Customer registered successfully");

    }

    public static void RegisterCustomerMenu()
    {
        while (true)
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine()!;
            break;
        }
        
        Console.Write("Enter customer ages: ");
        int ages = Convert.ToInt32(Console.ReadLine());
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
