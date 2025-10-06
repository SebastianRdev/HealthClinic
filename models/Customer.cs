namespace HealthClinic.models;

using HealthClinic.interfaces;
using HealthClinic.utils;

/// <summary>
/// Represents a client of the HealthClinic system. Contains personal information and the list of associated pets.
/// Implements interfaces for registration and notification.
/// </summary>
public class Customer : IRegistrable, IEntity, INotificable
{
    /// <summary>
    /// Unique customer identifier. Generated automatically when the instance is created.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();
    private string _name;
    private int _age;
    private string _address;
    private string _phone;
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
    /// Customer name. Cannot be empty.
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _name = value;
            else
                throw new ArgumentException("Name cannot be empty");
        }
    }

    /// <summary>
    /// Customer age. Must be a positive number.
    /// </summary>
    public int Age
    {
        get => _age;
        set
        {
            if (Validator.IsPositive(value))
                _age = value;
            else
                throw new ArgumentException("Age must be a positive number");
        }
    }

    /// <summary>
    /// Customer address. Cannot be empty.
    /// </summary>
    public string Address
    {
        get => _address;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _address = value;
            else
                throw new ArgumentException("Name cannot be empty");
        }
    }

    /// <summary>
    /// Customer's phone number. Cannot be empty.
    /// </summary>
    public string Phone
    {
        get => _phone;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _phone = value;
            else
                throw new ArgumentException("Invalid phone number format");
        }
    }

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
