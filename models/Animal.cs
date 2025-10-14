namespace HealthClinic.models;

/// <summary>
/// Represents a generic animal within the HealthClinic system. Serves as a base class for pets and other animals.
/// </summary>
public class Animal
{
    /// <summary>
    /// Unique identifier for the mascot. It is automatically generated when the instance is created.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    /// <summary>
    /// Animal name. Allows it to be identified within the system.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Age of the animal in years. Used for queries and filtering.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Animal species (example: dog, cat). Allows animals to be classified and grouped.
    /// </summary>
    public string? Species { get; set; }

    /// <summary>
    /// Static method that simulates the sound of an animal.
    /// </summary>
    public static void PlaySound()
    {
        Console.WriteLine($"\nThe dog makes a sound: Woof!");  
    }
}
