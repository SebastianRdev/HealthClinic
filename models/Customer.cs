namespace HealthClinic.models;

using HealthClinic.interfaces;
using HealthClinic.utils;

public class Customer : IRegistrable
{
    private Guid _id;
    private string _name;
    private int _age;
    private string _address;
    private string _phone;
    private List<Pet> _pets = [];

    // Constructor
    public Customer(string name, int age, string address, string phone, List<Pet>? pets)
    {
        _id = Guid.NewGuid();
        _name = name;
        _age = age;
        _address = address;
        _phone = phone;
        _pets = pets ?? new List<Pet>();
    }

    public Guid Id => _id;
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
    public string Address
    {
        get => _address;
        set => _address = value;
    }
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
    public List<Pet> Pets
    {
        get => _pets;
        private set => _pets = value ?? new List<Pet>();
    }

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

    public void Register()
    {
        Console.WriteLine($"\n✅ Customer {Name} registered successfully with ID: {Id}");
    }
}
