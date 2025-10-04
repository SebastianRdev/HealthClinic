namespace HealthClinic.models;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public Customer Owner { get; set; }

    // Constructor
    public Pet(string name, string species, string breed, int age)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Species = species;
        this.Breed = breed;
        this.Age = age;
    }

    public static void ShowInformation(Pet pet)
    {
        Console.WriteLine($"\n🐾 Pet ID: {pet.Id}");
        Console.WriteLine($"   Name: {pet.Name}");
        Console.WriteLine($"   Species: {pet.Species}");
        Console.WriteLine($"   Breed: {pet.Breed}");
        Console.WriteLine($"   Age: {pet.Age} years old");
        if (pet.Owner != null)
        {
            Console.WriteLine($"   Owner: {pet.Owner.Name} (ID: {pet.Owner.Id})");
        }
        else
        {
            Console.WriteLine("   Owner: None");
        }
    }
}
