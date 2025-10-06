namespace HealthClinic.models;

using HealthClinic.interfaces;

/// <summary>
/// Represents a veterinarian registered in the system. Includes identification information, name, and specialization.
/// </summary>
public class Veterinarian : IEntity
{
    /// <summary>
    /// Unique identifier for the veterinarian. It is automatically generated when the instance is created.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Name of the veterinarian.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Veterinarian's specialization (e.g., surgery, general medicine).
    /// </summary>
    public string? Specialization { get; set; }

    /// <summary>
    /// Constructor that initializes a new veterinarian with the provided data.
    /// </summary>
    /// <param name="name">Name of the veterinarian</param>
    /// <param name="specialization">Veterinary specialization</param>
    public Veterinarian(string name, string specialization)
    {
        Id = Guid.NewGuid();
        Name = name;
        Specialization = specialization;
    }
}