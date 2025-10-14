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

            // 🐾 Mostrar mascotas disponibles
            var pets = _appointmentService.GetAllPets();
            if (pets.Count == 0)
            {
                Console.WriteLine("⚠️ No pets registered. Please register a pet first.");
                return;
            }

            Console.WriteLine("\n🐕 --- Pet List ---");
            foreach (var pet in pets)
                Console.WriteLine($"ID: {pet.Id} | Name: {pet.Name} | Owner: {pet.Owner.Name}");

            string petInput = Validator.ValidateContent("\nEnter Pet ID: ");
            if (!Guid.TryParse(petInput, out Guid petId))
            {
                Console.WriteLine("⚠️ Invalid Pet ID format");
                return;
            }

            // 🩺 Mostrar veterinarios disponibles
            var vets = _appointmentService.GetAllVeterinarians();
            if (vets.Count == 0)
            {
                Console.WriteLine("⚠️ No veterinarians registered. Please register one first.");
                return;
            }

            Console.WriteLine("\n👨‍⚕️ --- Veterinarian List ---");
            foreach (var v in vets)
                Console.WriteLine($"ID: {v.Id} | Name: {v.Name} | Specialty: {v.Specialty}");

            string vetInput = Validator.ValidateContent("\nEnter Veterinarian ID: ");
            if (!Guid.TryParse(vetInput, out Guid vetId))
            {
                Console.WriteLine("⚠️ Invalid Veterinarian ID format");
                return;
            }

            // 📅 Fecha de cita
            DateTime date;
            while (true)
            {
                string dateInput = Validator.ValidateContent("Enter appointment date (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(dateInput, out date))
                {
                    if (date <= DateTime.Now)
                        Console.WriteLine("⚠️ The appointment date must be in the future.");
                    else break;
                }
                else
                {
                    Console.WriteLine("⚠️ Invalid date format. Example: 2025-10-15 14:30");
                }
            }

            // 💉 Servicios disponibles
            Console.WriteLine("\n💉 --- Available Services ---");
            foreach (var s in Enum.GetValues(typeof(ServiceType)))
                Console.WriteLine($"{(int)s}. {s}");

            int serviceInt = Validator.ValidatePositiveInt("\nEnter service number: ");
            if (!Enum.IsDefined(typeof(ServiceType), serviceInt))
            {
                Console.WriteLine("⚠️ Invalid service number");
                return;
            }
            var serviceType = (ServiceType)serviceInt;

            // 📝 Motivo
            string reason = Validator.ValidateContent("Enter appointment reason: ");

            // 🚫 Validar cita duplicada (misma fecha, mascota y veterinario)
            // if (_appointmentService.IsDuplicateAppointment(petId, vetId, date))
            // {
            //     Console.WriteLine("⚠️ There is already an appointment for this pet and veterinarian at the same time.");
            //     return;
            // }

            // ✅ Registrar cita
            var appointment = _appointmentService.RegisterAppointment(petId, vetId, date, serviceType, reason);
            Console.WriteLine($"\n✅ Appointment registered successfully with ID: {appointment.Id}");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Invalid input format. Please enter the data correctly.");
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

        try
        {
            ViewAppointmentsUI();

            string idInput = Validator.ValidateContent("\nEnter Appointment ID: ");
            if (!Guid.TryParse(idInput, out Guid appointmentId))
            {
                Console.WriteLine("⚠️ Invalid ID format");
                return;
            }

            // Obtener cita actual
            var appointment = _appointmentService.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("❌ No appointment found with that ID");
                return;
            }

            Console.WriteLine($"\nCurrent appointment details:");
            Console.WriteLine($"🐕 Pet ID: {appointment.PetId}");
            Console.WriteLine($"👨‍⚕️ Veterinarian ID: {appointment.VeterinarianId}");
            Console.WriteLine($"📅 Date: {appointment.DateTime:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"💉 Service: {appointment.ServiceType}");
            Console.WriteLine($"📝 Reason: {appointment.Reason}");

            Console.WriteLine("\nUpdate fields (y/n):");

            Guid? newPetId = appointment.PetId;
            if (Validator.AskYesNo("Change pet? (y/n): "))
            {
                var pets = _appointmentService.GetAllPets();
                if (pets.Count == 0)
                {
                    Console.WriteLine("⚠️  No pets available. Please register one first");
                    return;
                }

                Console.WriteLine("\n--- Pet List ---");
                foreach (var pet in pets)
                    Console.WriteLine($"ID: {pet.Id} | Name: {pet.Name}");

                string petInput = Validator.ValidateContent("Enter new Pet ID: ");
                if (Guid.TryParse(petInput, out Guid petId))
                    newPetId = petId;
                else
                    Console.WriteLine("⚠️  Invalid Pet ID format. Pet not changed");
            }

            Guid? newVetId = appointment.VeterinarianId;
            if (Validator.AskYesNo("Change veterinarian? (y/n): "))
            {
                var vets = _appointmentService.GetAllVeterinarians();
                if (vets.Count == 0)
                {
                    Console.WriteLine("⚠️  No veterinarians available. Please register one first");
                    return;
                }

                Console.WriteLine("\n--- Veterinarian List ---");
                foreach (var v in vets)
                    Console.WriteLine($"ID: {v.Id} | Name: {v.Name} | Specialty: {v.Specialty}");

                string vetInput = Validator.ValidateContent("Enter new Veterinarian ID: ");
                if (Guid.TryParse(vetInput, out Guid vetId))
                    newVetId = vetId;
                else
                    Console.WriteLine("⚠️ Invalid Vet ID format. Veterinarian not changed.");
            }

            DateTime newDate = appointment.DateTime;
            if (Validator.AskYesNo("Change date? (y/n): "))
            {
                string dateInput = Validator.ValidateContent("Enter new date (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(dateInput, out DateTime date))
                    newDate = date;
                else
                    Console.WriteLine("⚠️ Invalid date format. Date not changed.");
            }

            ServiceType newService = appointment.ServiceType;
            if (Validator.AskYesNo("Change service type? (y/n): "))
            {
                Console.WriteLine("\n--- Available Services ---");
                foreach (var s in Enum.GetValues(typeof(ServiceType)))
                    Console.WriteLine($"{(int)s}. {s}");

                int serviceInt = Validator.ValidatePositiveInt("Select service number: ");
                if (Enum.IsDefined(typeof(ServiceType), serviceInt))
                    newService = (ServiceType)serviceInt;
                else
                    Console.WriteLine("⚠️ Invalid service number. Service not changed.");
            }

            string newReason = appointment.Reason;
            if (Validator.AskYesNo("Change reason? (y/n): "))
                newReason = Validator.ValidateContent("📝 Enter new reason: ");

            _appointmentService.UpdateAppointment(
                appointmentId,
                newPetId,
                newVetId,
                newDate,
                newService,
                newReason
            );

            Console.WriteLine("\n✅ Appointment updated successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("⚠️ Invalid format. Please enter valid data.");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("❌ Appointment not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error updating appointment: {ex.Message}");
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

