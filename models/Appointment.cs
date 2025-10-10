namespace HealthClinic.models;

using HealthClinic.interfaces;
using HealthClinic.models.Enums;

/// <summary>
/// Represents an appointment between a pet, its owner, and a veterinarian.
/// </summary>
public class Appointment : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime DateTime { get; set; }

    public string Reason { get; set; } = string.Empty;

    public Guid PetId { get; set; }

    public Guid VeterinarianId { get; set; }

    public ServiceType ServiceType { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public string Notes { get; set; } = string.Empty;

    // 🔽 Relaciones directas opcionales
    public Pet? Pet { get; set; }
    public Veterinarian? Veterinarian { get; set; }
    public bool IsAttended => Status == AppointmentStatus.Completed;


    public Appointment(Guid petId, Guid veterinarianId, DateTime dateTime, ServiceType serviceType, string reason)
    {
        PetId = petId;
        VeterinarianId = veterinarianId;
        DateTime = dateTime;
        ServiceType = serviceType;
        Reason = reason;
        Status = AppointmentStatus.Scheduled;
    }
}

