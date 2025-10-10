namespace HealthClinic.models;

using HealthClinic.interfaces;
using HealthClinic.models.Enums;

/// <summary>
/// Represents a veterinarian registered in the system. Includes identification information, name, and specialization.
/// </summary>
public class Veterinarian : Person, IEntity, IRegistrable
{
    public string Email { get; set; }

    public string LicenseNumber { get; set; }
    public Specialties Specialty { get; set; }

    public bool IsActive { get; set; } = true;


    /// <summary>
    /// Creates a new Veterinarian instance with the provided information.
    /// </summary>
    public Veterinarian(string name, int age, string address, string phone, string email, string licenseNumber, Specialties specialty)
    {
        Name = name;
        Age = age;
        Address = address;
        Phone = phone;
        Email = email;
        LicenseNumber = licenseNumber;
        Specialty = specialty;
        IsActive = true;
    }

    /// <summary>
    /// Registers the veterinarian in the system.
    /// </summary>
    public void Register()
    {
        Console.WriteLine($"\n✅ Veterinarian registered successfully");
    }
}