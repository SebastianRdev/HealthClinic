namespace HealthClinic.models;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public Customer Customer { get; set; }

    // Constructor
    public Pet(string name, string species, string breed, int age)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Species = species;
        this.Breed = breed;
        this.Age = age;
    }
}
