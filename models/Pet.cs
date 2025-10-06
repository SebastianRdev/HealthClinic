namespace HealthClinic.models;

using HealthClinic.interfaces;

/// <summary>
/// Represents a pet registered in the system. It inherits from Animal and adds specific information such as breed, owner, and unique identification.
/// </summary>
public class Pet : Animal, IRegistrable, IEntity
{
    /// <summary>
    /// Unique identifier for the mascot. It is automatically generated when the instance is created.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Pet breed. Allows you to classify it within its species.
    /// </summary>
    public string Breed { get; set; }

    /// <summary>
    /// Pet owner. Links the pet to a customer in the system.
    /// </summary>
    public Customer? Owner { get; set; }

    /// <summary>
    /// Constructor that initializes a new pet with the provided data.
    /// </summary>
    /// <param name="name">Pet's name</param>
    /// <param name="species">Pet species</param>
    /// <param name="breed">Pet breed</param>
    /// <param name="age">Age of the pet</param>
    public Pet(string name, string species, string breed, int age)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Species = species;
        this.Breed = breed;
        this.Age = age;
    }

    /// <summary>
    /// Displays detailed information about the pet, including the owner, if applicable.
    /// </summary>
    /// <param name="pet">Pet to show</param>
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

    /// <summary>
    /// It shows the characteristic sounds of dogs and cats.
    /// </summary>
    public new static void PlaySound()
    {
        Console.WriteLine($"\nThe dog makes a sound: Woof!");
        Console.WriteLine($"\nThe cat makes a sound: Meow!");
    }

    /// <summary>
    /// Register the pet in the system, displaying a confirmation message.
    /// </summary>
    public void Register()
    {
        Console.WriteLine($"\n✅ Pet {Name} registered successfully with ID: {Id}");
    }
}
