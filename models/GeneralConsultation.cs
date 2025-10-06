namespace HealthClinic.models;

/// <summary>
/// Represents a general consultation performed by a veterinarian. It inherits the logic of veterinary service.
/// </summary>
public class GeneralConsultation : VeterinarianService
{
    /// <summary>
    /// Perform general consultation care, displaying the corresponding message.
    /// </summary>
    public override void Attend()
    {
        Console.WriteLine("Conducting a general consultation...");
    }
}
