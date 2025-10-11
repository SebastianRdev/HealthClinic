namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.models.Enums;
using HealthClinic.repositories;
using HealthClinic.utils;

/// <summary>
/// Service that manages appointment-related business logic.
/// </summary>
public class AppointmentService
{
    /// <summary>
    /// Registers a new appointment interactively.
    /// </summary>
    public static Appointment? RegisterAppointment(
        Repository<Pet> petRepo,
        Repository<Veterinarian> vetRepo,
        Repository<Appointment> appointmentRepo)
    {
        Console.WriteLine("\n--- 🗓️ Register Appointment ---");

        var pets = petRepo.GetAll().ToList();
        if (!Validator.IsExist(pets, "⚠️ No pets registered.")) return null;

        Console.WriteLine("\n--- 🐾 Pet List ---");
        foreach (var pet in pets)
            Console.WriteLine($"ID: {pet.Id} | Name: {pet.Name}");

        Console.Write("\nEnter Pet ID: ");
        string petIdInput = Console.ReadLine()!.Trim();
        var petObj = pets.FirstOrDefault(p => p.Id.ToString() == petIdInput);
        if (!Validator.IsExist(petObj, "❌ Pet not found")) return null;
        if (petObj == null) return null;

        var vets = vetRepo.GetAll().ToList();
        if (!Validator.IsExist(vets, "⚠️ No veterinarians registered.")) return null;

        Console.WriteLine("\n--- 🩺 Veterinarian List ---");
        foreach (var vet in vets)
            Console.WriteLine($"ID: {vet.Id} | Name: {vet.Name}");

        Console.Write("\nEnter Veterinarian ID: ");
        string vetIdInput = Console.ReadLine()!.Trim();
        var vetObj = vets.FirstOrDefault(v => v.Id.ToString() == vetIdInput);
        if (!Validator.IsExist(vetObj, "❌ Veterinarian not found")) return null;
        if (vetObj == null) return null;

        Console.Write("\nEnter appointment date (yyyy-MM-dd HH:mm): ");
        string dateInput = Console.ReadLine()!.Trim();
        if (!DateTime.TryParse(dateInput, out DateTime dateTime))
        {
            Console.WriteLine("❌ Invalid date format");
            return null;
        }

        Console.WriteLine("\nAvailable Services:");
        foreach (var service in Enum.GetValues(typeof(ServiceType)))
            Console.WriteLine($"{(int)service}. {service}");

        Console.Write("Select service number: ");
        string serviceInput = Console.ReadLine()!.Trim();
        if (!int.TryParse(serviceInput, out int serviceInt) || !Enum.IsDefined(typeof(ServiceType), serviceInt))
        {
            Console.WriteLine("❌ Invalid service type");
            return null;
        }
        ServiceType serviceType = (ServiceType)serviceInt;

        Console.Write("\nEnter reason for the appointment: ");
        string reason = Console.ReadLine() ?? "";

        var appointment = new Appointment(petObj.Id, vetObj.Id, dateTime, serviceType, reason);
        appointmentRepo.Add(appointment);

        Console.WriteLine("\n✅ Appointment registered successfully!");
        return appointment;
    }

    /// <summary>
    /// Displays all appointments.
    /// </summary>
    public static void ViewAppointments(List<Appointment> appointments)
    {
        if (appointments == null || appointments.Count == 0)
        {
            Console.WriteLine("⚠️  No appointments registered");
            return;
        }

        Console.WriteLine("\n--- 📋 Appointments List ---");
        foreach (var a in appointments)
        {
            Console.WriteLine($"\n🆔 Appointment ID: {a.Id}");
            Console.WriteLine($"🐾 Pet ID: {a.PetId}");
            Console.WriteLine($"🩺 Veterinarian ID: {a.VeterinarianId}");
            Console.WriteLine($"📅 Date: {a.DateTime}");
            Console.WriteLine($"🧼 Service: {a.ServiceType}");
            Console.WriteLine($"🗒️ Reason: {a.Reason}");
            Console.WriteLine($"📌 Status: {a.Status}");
        }
    }

    /// <summary>
    /// Updates appointment status.
    /// </summary>
    public static void UpdateAppointmentStatus(List<Appointment> appointments)
    {
        Console.WriteLine("\n--- 🔄 Update Appointment Status ---");
        ViewAppointments(appointments);

        Console.Write("\nEnter Appointment ID: ");
        string idInput = Console.ReadLine()!.Trim();
        var appointment = appointments.FirstOrDefault(a => a.Id.ToString() == idInput);
        if (!Validator.IsExist(appointment, "❌ Appointment not found")) return;
        if (appointment == null) return;

        Console.WriteLine("\nPossible statuses:");
        foreach (var status in Enum.GetValues(typeof(AppointmentStatus)))
            Console.WriteLine($"{(int)status}. {status}");

        Console.Write("Enter new status number: ");
        string statusInput = Console.ReadLine()!.Trim();
        if (!int.TryParse(statusInput, out int statusInt) || !Enum.IsDefined(typeof(AppointmentStatus), statusInt))
        {
            Console.WriteLine("❌ Invalid status");
            return;
        }
        appointment.Status = (AppointmentStatus)statusInt;

        Console.WriteLine($"✅ Appointment status updated to {appointment.Status}.");
    }

    /// <summary>
    /// Removes an appointment from the list.
    /// </summary>
    public static void RemoveAppointment(List<Appointment> appointments)
    {
        Console.WriteLine("\n--- 🗑️ Remove Appointment ---");
        ViewAppointments(appointments);

        Console.Write("\nEnter Appointment ID to remove: ");
        string idInput = Console.ReadLine()!.Trim();
        var appointment = appointments?.FirstOrDefault(a => a.Id.ToString() == idInput);
        if (appointment == null)
        {
            Console.WriteLine("❌ Appointment not found");
            return;
        }
        appointments?.Remove(appointment);

        Console.WriteLine($"✅ Appointment removed successfully");
    }

    /// <summary>
    /// Updates the details of an appointment interactively.
    /// </summary>
    public static void UpdateAppointment(
        List<Appointment> appointments,
        Repository<Pet> petRepo,
        Repository<Veterinarian> vetRepo)
    {
        Console.WriteLine("\n--- ✏️ Update Appointment ---");
        ViewAppointments(appointments);

        Console.Write("\nEnter Appointment ID to update: ");
        string idInput = Console.ReadLine()!.Trim();
        var appointment = appointments?.FirstOrDefault(a => a.Id.ToString() == idInput);
        if (appointment == null)
        {
            Console.WriteLine("❌ Appointment not found");
            return;
        }

        UpdatePetIfRequested(appointment, petRepo);
        UpdateVetIfRequested(appointment, vetRepo);
        UpdateDateIfRequested(appointment);
        UpdateServiceIfRequested(appointment);
        UpdateReasonIfRequested(appointment);

        Console.WriteLine("\n✅ Appointment updated successfully.");
    }

    private static void UpdatePetIfRequested(Appointment appointment, Repository<Pet> petRepo)
    {
        Console.WriteLine("\nUpdate Pet? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() != "y") return;

        var pets = petRepo.GetAll().ToList();
        Console.WriteLine("\n--- 🐾 Pet List ---");
        foreach (var pet in pets)
            Console.WriteLine($"ID: {pet.Id} | Name: {pet.Name}");

        Console.Write("Enter new Pet ID: ");
        string petIdInput = Console.ReadLine()!.Trim();
        var petObj = pets.FirstOrDefault(p => p.Id.ToString() == petIdInput);

        if (Validator.IsExist(petObj, "❌ Pet not found"))
            appointment.PetId = petObj!.Id;
    }

    private static void UpdateVetIfRequested(Appointment appointment, Repository<Veterinarian> vetRepo)
    {
        Console.WriteLine("\nUpdate Veterinarian? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() != "y") return;

        var vets = vetRepo.GetAll().Where(v => v.IsActive).ToList();
        Console.WriteLine("\n--- 🩺 Veterinarian List ---");
        foreach (var vet in vets)
            Console.WriteLine($"ID: {vet.Id} | Name: {vet.Name}");

        Console.Write("Enter new Veterinarian ID: ");
        string vetIdInput = Console.ReadLine()!.Trim();
        var vetObj = vets.FirstOrDefault(v => v.Id.ToString() == vetIdInput);

        if (Validator.IsExist(vetObj, "❌ Veterinarian not found"))
            appointment.VeterinarianId = vetObj!.Id;
    }

    private static void UpdateDateIfRequested(Appointment appointment)
    {
        Console.WriteLine("\nUpdate Date? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() != "y") return;

        Console.Write("Enter new appointment date (yyyy-MM-dd HH:mm): ");
        string dateInput = Console.ReadLine()!.Trim();
        if (DateTime.TryParse(dateInput, out DateTime dateTime))
            appointment.DateTime = dateTime;
        else
            Console.WriteLine("❌ Invalid date format. Date not updated");
    }

    private static void UpdateServiceIfRequested(Appointment appointment)
    {
        Console.WriteLine("\nUpdate Service? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() != "y") return;

        Console.WriteLine("\nAvailable Services:");
        foreach (var service in Enum.GetValues(typeof(ServiceType)))
            Console.WriteLine($"{(int)service}. {service}");

        Console.Write("Select new service number: ");
        string serviceInput = Console.ReadLine()!.Trim();

        if (int.TryParse(serviceInput, out int serviceInt) && Enum.IsDefined(typeof(ServiceType), serviceInt))
            appointment.ServiceType = (ServiceType)serviceInt;
        else
            Console.WriteLine("❌ Invalid service type. Service not updated");
    }

    private static void UpdateReasonIfRequested(Appointment appointment)
    {
        Console.WriteLine("\nUpdate Reason? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() != "y") return;

        Console.Write("Enter new reason: ");
        appointment.Reason = Console.ReadLine() ?? "";
    }
}