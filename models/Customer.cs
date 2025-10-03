namespace HealthClinic.models;


public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public List<Pet> Pets { get; set; } = [];

    // Constructor
    public Customer(string name, int age, string address, string phone, List<Pet>? pets)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Address = address;
        Phone = phone;
        Pets = pets ?? new List<Pet>();
    }
}
