namespace HealthClinic.menus;

using HealthClinic.utils;
using HealthClinic.services;
using HealthClinic.models.Enums;

public class AppointmentMenu
{
    private readonly AppointmentService _appointmentService;

    public AppointmentMenu(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public void AppointmentMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AppointmentsCRUD();
                        continue;
                    case 2:
                        Console.WriteLine("\nBack to main menu");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
                        continue;
                }
            }
            catch
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a number");
                continue;
            }
            break;
        }
    }

    public void AppointmentsCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsCRUD();
                Console.Write("\n👉 Enter your choice: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\n❌ Invalid input. Please enter a number");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        RegisterAppointmentUI();
                        continue;
                    case 2:
                        ViewAppointmentsUI();
                        continue;
                    case 3:
                        UpdateAppointmentUI();
                        continue;
                    case 4:
                        RemoveAppointmentUI();
                        continue;
                    case 5:
                        Console.WriteLine("\nBack to main menu");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
                        continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ {ex.Message}");
                continue;
            }
            break;
        }
    }

    private void RegisterAppointmentUI()
    {
        try
        {
            Console.WriteLine("\n--- 🗓️  Register Appointment ---");

            // 📋 Mostrar mascotas disponibles
            var pets = _appointmentService.GetAllPets();
            if (pets.Count == 0)
            {
                Console.WriteLine("⚠️ No pets registered. Please register a pet first");
                return;
            }

            Console.WriteLine("\n🐾 --- Pet List ---");
            foreach (var p in pets)
                Console.WriteLine($"ID: {p.Id} | Name: {p.Name} | Owner ID: {p.Owner.Id}");

            Console.Write("\nEnter Pet ID: ");
            Guid petId = Guid.Parse(Console.ReadLine()!.Trim());

            // 🩺 Mostrar veterinarios activos
            var vets = _appointmentService.GetAllVeterinarians();
            if (vets.Count == 0)
            {
                Console.WriteLine("⚠️ No veterinarians available. Please register one first");
                return;
            }

            Console.WriteLine("\n🩺 --- Veterinarian List ---");
            foreach (var v in vets)
                Console.WriteLine($"ID: {v.Id} | Name: {v.Name}");

            Console.Write("\nEnter Vet ID: ");
            Guid vetId = Guid.Parse(Console.ReadLine()!.Trim());

            // 📅 Fecha
            Console.Write("Enter date (yyyy-MM-dd HH:mm): ");
            DateTime date = DateTime.Parse(Console.ReadLine()!.Trim());

            // 🧼 Mostrar servicios disponibles
            Console.WriteLine("\n🧼 --- Available Services ---");
            foreach (var service in Enum.GetValues(typeof(ServiceType)))
                Console.WriteLine($"{(int)service}. {service}");

            Console.Write("\nEnter service type (number): ");
            int serviceInt = int.Parse(Console.ReadLine()!.Trim());
            var serviceType = (ServiceType)serviceInt;

            // 🗒️ Motivo
            Console.Write("Enter reason: ");
            string reason = Console.ReadLine()!.Trim();

            // Registrar cita
            var appointment = _appointmentService.RegisterAppointment(petId, vetId, date, serviceType, reason);
            Console.WriteLine($"\n✅ Appointment registered successfully with ID: {appointment.Id}");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Invalid input format. Please enter the data correctly");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex.Message}");
        }
    }

    private void ViewAppointmentsUI()
    {
        var appointments = _appointmentService.ViewAppointments().OrderBy(a => a.DateTime).ToList();
        if (appointments.Count == 0)
        {
            Console.WriteLine("⚠️  No appointments registered");
            return;
        }

        foreach (var a in appointments)
        {
            Console.WriteLine($"\n🆔 {a.Id}");
            Console.WriteLine($"🐾 Pet ID: {a.PetId}");
            Console.WriteLine($"🩺 Vet ID: {a.VeterinarianId}");
            Console.WriteLine($"📅 Date: {a.DateTime}");
            Console.WriteLine($"🧼 Service: {a.ServiceType}");
            Console.WriteLine($"🗒️ Reason: {a.Reason}");
        }
    }

    private void UpdateAppointmentUI()
    {
        Console.WriteLine("\n--- ✏️  Update Appointment ---");

        ViewAppointmentsUI();

        Console.Write("\nEnter Appointment ID: ");
        var idInput = Console.ReadLine();

        if (!Guid.TryParse(idInput, out Guid appointmentId))
        {
            Console.WriteLine("⚠️  Invalid ID format");
            return;
        }

        Guid? newPetId = null;
        Guid? newVetId = null;
        DateTime? newDate = null;
        ServiceType? newService = null;
        string? newReason = null;

        // Update pet
        Console.Write("Update Pet? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new Pet ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid petId))
                newPetId = petId;
            else
                Console.WriteLine("⚠️  Invalid Pet ID format");
        }

        // Update vet
        Console.Write("Update Veterinarian? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new Vet ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid vetId))
                newVetId = vetId;
            else
                Console.WriteLine("⚠️  Invalid Vet ID format");
        }

        // Update date
        Console.Write("Update Date? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new date (yyyy-MM-dd HH:mm): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                newDate = date;
            else
                Console.WriteLine("⚠️  Invalid date format");
        }

        // Update service
        Console.Write("Update Service Type? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.WriteLine("\nAvailable Services:");
            foreach (var s in Enum.GetValues(typeof(ServiceType)))
                Console.WriteLine($"{(int)s}. {s}");

            Console.Write("Select service number: ");
            if (int.TryParse(Console.ReadLine(), out int serviceInt) &&
                Enum.IsDefined(typeof(ServiceType), serviceInt))
                newService = (ServiceType)serviceInt;
            else
                Console.WriteLine("⚠️  Invalid service number");
        }

        // Update reason
        Console.Write("Update Reason? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new reason: ");
            newReason = Console.ReadLine();
        }

        try
        {
            _appointmentService.UpdateAppointment(
                appointmentId,
                newPetId,
                newVetId,
                newDate,
                newService,
                newReason
            );

            Console.WriteLine("\n✅ Appointment updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }



    private void RemoveAppointmentUI()
    {
        Console.WriteLine("\n--- ❌ Remove Appointment ---");

        ViewAppointmentsUI();

        Console.Write("\nEnter Appointment ID to remove: ");
        var appointmentIdInput = Console.ReadLine();

        if (!Guid.TryParse(appointmentIdInput, out Guid appointmentId))
        {
            Console.WriteLine("⚠️ Invalid ID format.");
            return;
        }

        try
        {
            _appointmentService.RemoveAppointment(appointmentId);
            Console.WriteLine("✅ Appointment removed successfully!");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("⚠️ Appointment not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error removing appointment: {ex.Message}");
        }
    }
}

