namespace HealthClinic.models;


public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Pet> Pets { get; set; } = [];

    public Customer(string name, int age, Pet pet)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Pets = new List<Pet> { pet };
    }
}
