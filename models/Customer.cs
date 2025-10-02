namespace HealthClinic.models;


public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Pet> Pets { get; set; } = [];

    public Customer(string name, int age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
    }

    public void AddPet(Pet pet)
    {
        pet.Customer = this;
        Pets.Add(pet);
    }
}
