namespace HealthClinic.models;

/// <summary>
/// Represents the vaccination service performed by a veterinarian. It inherits the logic of the veterinary service.
/// </summary>
public class Vaccination : VeterinarianService
{
    /// <summary>
    /// Perform the vaccination, displaying the corresponding message.
    /// </summary>
    public override void Attend()
    {
        Console.WriteLine("Administering a vaccination...");
    }
}
