namespace HealthClinic.services;

/// <summary>
/// Abstract base class for veterinary services in the HealthClinic system. Defines the care method that each specific service must implement.
/// </summary>
public abstract class VeterinaryService
{
    /// <summary>
    /// Abstract method representing the care provided by the veterinary service.
    /// </summary>
    public abstract void Attend();
}
