namespace HealthClinic.models;

using HealthClinic.interfaces;

/// <summary>
/// Represents a client of the HealthClinic system. Contains personal information and the list of associated pets.
/// Implements interfaces for registration and notification.
/// </summary>
public class Customer : Person, IRegistrable, IEntity, INotificable
{
    private List<Pet> _pets = [];

    /// <summary>
    /// Constructor that initializes a new client with the provided data and their list of pets.
    /// </summary>
    /// <param name="name">Customer name</param>
    /// <param name="age">Customer age</param>
    /// <param name="address">Customer address</param>
    /// <param name="phone">Customer's phone</param>
    /// <param name="pets">List of associated pets</param>
    public Customer(string name, int age, string address, string phone, List<Pet>? pets)
    {
        _name = name;
        _age = age;
        _address = address;
        _phone = phone;
        _pets = pets ?? new List<Pet>();
    }

    /// <summary>
    /// Customer name.
    /// </summary>
    public string Name { get => _name; set => _name = value; }


    /// <summary>
    /// Customer age.
    /// </summary>
    public int Age { get => _age; set => _age = value; }

    /// <summary>
    /// Customer address
    /// </summary>
    public string Address { get => _address; set => _address = value; }

    /// <summary>
    /// Customer's phone number.
    /// </summary>
    public string Phone { get => _phone; set => _phone = value; }

    /// <summary>
    /// List of pets associated with the customer.
    /// </summary>
    public List<Pet> Pets
    {
        get => _pets;
        private set => _pets = value ?? new List<Pet>();
    }

    /// <summary>
    /// Displays detailed customer information, including their pets.
    /// </summary>
    /// <param name="customer">Customer to display</param>
    public static void ShowInformation(Customer customer)
    {
        Console.WriteLine($"\n👤 Customer ID: {customer.Id}");
        Console.WriteLine($"   Name: {customer.Name}");
        Console.WriteLine($"   Age: {customer.Age}");
        Console.WriteLine($"   Address: {customer.Address}");
        Console.WriteLine($"   Phone: {customer.Phone}");
        if (customer.Pets.Count > 0)
        {
            Console.WriteLine("Pets:");
            foreach (var pet in customer.Pets)
            {
                Console.WriteLine($"  - {pet.Name}, ({pet.Age} years old)");
            }
        }
        else
        {
            Console.WriteLine("Pets: None");
        }
    }

    /// <summary>
    /// Display a confirmation message when registering the customer in the system.
    /// </summary>
    public void Register()
    {
        Console.WriteLine($"\n✅ Customer {Name} registered successfully with ID: {Id}");
    }

    /// <summary>
    /// Send a notification to the customer. Implementation pending.
    /// </summary>
    public void SendNotification()
    {
        // Implementation
    }
}
