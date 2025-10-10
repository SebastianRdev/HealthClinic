namespace HealthClinic.data;

using HealthClinic.models;

/// <summary>
/// Class that simulates the in-memory database for the HealthClinic system.
/// Stores lists and dictionaries of clients, pets, and veterinarians.
/// </summary>
public class Database
{
    /// <summary>
    /// Dictionary of clients indexed by Guid for quick access.
    /// </summary>
    public static Dictionary<Guid, Customer> CustomersDict { get; } = new();

    /// <summary>
    /// List of pets registered in the system.
    /// </summary>
    public static List<Pet> Pets { get; } = [];

    /// <summary>
    /// List of veterinarians registered in the system.
    /// </summary>
    public static List<Veterinarian> Veterinarians { get; } = [];

    /// <summary>
    /// List of Appointment registered in the system.
    /// </summary>
    public static List<Appointment> Appointments { get; } = [];
}