namespace HealthClinic.data;

using HealthClinic.models;
public class Database
{
    public static List<Customer> Customers { get; } = [];
    public static Dictionary<Guid, Customer> CustomersDict { get; } = new();
    public static List<Pet> Pets { get; } = [];
    public static List<Veterinarian> Veterinarians { get; } = [];
}