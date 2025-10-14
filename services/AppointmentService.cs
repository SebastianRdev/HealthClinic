namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.models.Enums;
using HealthClinic.repositories;

/// <summary>
/// Service that manages appointment-related business logic.
/// </summary>
public class AppointmentService
{
    private readonly IRepository<Pet> _petRepo;
    private readonly IRepository<Veterinarian> _vetRepo;
    private readonly IRepository<Appointment> _appointmentRepo;

    public AppointmentService(
        IRepository<Pet> petRepo,
        IRepository<Veterinarian> vetRepo,
        IRepository<Appointment> appointmentRepo)
    {
        _petRepo = petRepo;
        _vetRepo = vetRepo;
        _appointmentRepo = appointmentRepo;
    }

    /// <summary>
    /// Registers a new appointment interactively.
    /// </summary>
    public Appointment RegisterAppointment(Guid petId, Guid vetId, DateTime date, ServiceType serviceType, string reason)
    {
        // Validations
        var pet = _petRepo.GetById(petId) ?? throw new KeyNotFoundException("Pet not found");
        var vet = _vetRepo.GetById(vetId) ?? throw new KeyNotFoundException("Veterinarian not found");

        if (date < DateTime.Now)
            throw new ArgumentException("The appointment date cannot be in the past", nameof(date));

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason is required", nameof(reason));

        // New appointment
        var appointment = new Appointment(pet.Id, vet.Id, date, serviceType, reason);

        _appointmentRepo.Add(appointment);
        return appointment;
    }

    /// <summary>
    /// Returns all appointments from the repository.
    /// </summary>
    public List<Appointment> ViewAppointments()
    {
        return _appointmentRepo.GetAll().ToList();
    }

    /// <summary>
    /// Updates appointment status.
    /// </summary>
    // public static void UpdateAppointmentStatus(List<Appointment> appointments)
    // {
    //     Console.WriteLine("\n--- 🔄 Update Appointment Status ---");
    //     ViewAppointments(appointments);

    //     Console.Write("\nEnter Appointment ID: ");
    //     string idInput = Console.ReadLine()!.Trim();
    //     var appointment = appointments.FirstOrDefault(a => a.Id.ToString() == idInput);
    //     if (!Validator.IsExist(appointment, "❌ Appointment not found")) return;
    //     if (appointment == null) return;

    //     Console.WriteLine("\nPossible statuses:");
    //     foreach (var status in Enum.GetValues(typeof(AppointmentStatus)))
    //         Console.WriteLine($"{(int)status}. {status}");

    //     Console.Write("Enter new status number: ");
    //     string statusInput = Console.ReadLine()!.Trim();
    //     if (!int.TryParse(statusInput, out int statusInt) || !Enum.IsDefined(typeof(AppointmentStatus), statusInt))
    //     {
    //         Console.WriteLine("❌ Invalid status");
    //         return;
    //     }
    //     appointment.Status = (AppointmentStatus)statusInt;

    //     Console.WriteLine($"✅ Appointment status updated to {appointment.Status}.");
    // }

    /// <summary>
    /// Removes an appointment by its ID.
    /// </summary>
    public void RemoveAppointment(Guid appointmentId)
    {
        var appointment = _appointmentRepo.GetById(appointmentId)
            ?? throw new KeyNotFoundException("Appointment not found");

        _appointmentRepo.Remove(appointment.Id);
    }

    /// <summary>
    /// Updates an existing appointment with new values.
    /// </summary>
    public void UpdateAppointment(
        Guid appointmentId,
        Guid? newPetId = null,
        Guid? newVetId = null,
        DateTime? newDate = null,
        ServiceType? newService = null,
        string? newReason = null)
    {
        var appointment = _appointmentRepo.GetById(appointmentId)
            ?? throw new KeyNotFoundException("Appointment not found");

        if (newPetId.HasValue)
        {
            var pet = _petRepo.GetById(newPetId.Value)
                ?? throw new KeyNotFoundException("Pet not found");
            appointment.PetId = pet.Id;
        }

        if (newVetId.HasValue)
        {
            var vet = _vetRepo.GetById(newVetId.Value)
                ?? throw new KeyNotFoundException("Veterinarian not found");
            appointment.VeterinarianId = vet.Id;
        }

        if (newDate.HasValue)
        {
            if (newDate.Value < DateTime.Now)
                throw new ArgumentException("Date cannot be in the past");
            appointment.DateTime = newDate.Value;
        }

        if (newService.HasValue)
            appointment.ServiceType = newService.Value;

        if (!string.IsNullOrWhiteSpace(newReason))
            appointment.Reason = newReason;

        _appointmentRepo.Update(appointment);
    }

    /// <summary>
    /// Gets all pets from the repository.
    /// </summary>
    /// <returns>A list of all pets.</returns>
    public List<Pet> GetAllPets()
    {
        return _petRepo.GetAll().ToList();
    }

    /// <summary>
    /// Gets all active veterinarians from the repository.
    /// </summary>
    /// <returns>A list of all active veterinarians.</returns>
    public List<Veterinarian> GetAllVeterinarians()
    {
        return _vetRepo.GetAll().Where(v => v.IsActive).ToList();
    }

}