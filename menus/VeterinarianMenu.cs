namespace HealthClinic.menus;

using HealthClinic.utils;
using HealthClinic.services;
using HealthClinic.models.Enums;
public class VeterinarianMenu
{
    private readonly VeterinarianService _veterinarianService;

    public VeterinarianMenu(VeterinarianService veterinarianService)
    {
        _veterinarianService = veterinarianService;
    }

    public void VeterinarianMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowVeterinarianMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        VeterinarianCRUD();
                        continue;
                    case 2:
                        AppointmentsVeterinarianMenu();
                        continue;
                    case 3:
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

    public void VeterinarianCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowVeterinarianCRUD();
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
                        RegisterVeterinarianUI();
                        continue;
                    case 2:
                        ViewVeterinariansUI();
                        continue;
                    case 3:
                        UpdateVeterinarianUI();
                        continue;
                    case 4:
                        RemoveVeterinarianUI();
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

    public static void AppointmentsVeterinarianMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsVeterinarianMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        //
                        continue;
                    case 2:
                        //
                        continue;
                    case 3:
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

    private void RegisterVeterinarianUI()
    {
        Console.WriteLine("\n--- 📝 Register Veterinarian 👨‍⚕️ ---");

        try
        {
            string vetName = Validator.ValidateContent("\n👤 Enter veterinarian's name: ");
            int vetAge = Validator.ValidatePositiveInt("🎂 Enter veterinarian's age: ");
            string vetAddress = Validator.ValidateContent("🏠 Enter veterinarian's address: ");
            string vetPhone = Validator.ValidateContent("📞 Enter veterinarian's phone: ");
            string vetEmail = Validator.ValidateContent("📧 Enter veterinarian's email: ");

            Console.WriteLine("\n🧼 --- Specialties ---");
            foreach (var specialty in Enum.GetValues(typeof(Specialties)))
                Console.WriteLine($"{(int)specialty}. {specialty}");

            int specialtyInt = Validator.ValidatePositiveInt("\nEnter specialty (number): ");
            if (!Enum.IsDefined(typeof(Specialties), specialtyInt))
            {
                Console.WriteLine("⚠️  Invalid specialty number");
                return;
            }
            var specialtyType = (Specialties)specialtyInt;

            string vetLicense = Validator.ValidateContent("🔢 Enter license number: ");

            var veterinarian = _veterinarianService.RegisterVeterinarian(
                vetName,
                vetAge,
                vetAddress,
                vetPhone,
                vetEmail,
                specialtyType,
                vetLicense
            );

            Console.WriteLine($"\n✅ Veterinarian registered successfully with ID: {veterinarian.Id}");
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


    private void ViewVeterinariansUI()
    {
        var veterinarians = _veterinarianService.ViewVeterinarians().ToList();
        if (veterinarians.Count == 0)
        {
            Console.WriteLine("⚠️  No veterinarians registered");
            return;
        }

        foreach (var vet in veterinarians)
        {
            Console.WriteLine($"\n🆔 ID: {vet.Id}");
            Console.WriteLine($"👤 Name: {vet.Name}");
            Console.WriteLine($"🎂 Age: {vet.Age}");
            Console.WriteLine($"🏠 Address: {vet.Address}");
            Console.WriteLine($"📞 Phone: {vet.Phone}");
            Console.WriteLine($"📧 Email: {vet.Email}");
            Console.WriteLine($"🔢 License: {vet.LicenseNumber}");
            Console.WriteLine($"🩺 Specialty: {vet.Specialty}");
            Console.WriteLine($"✅ Active: {(vet.IsActive ? "Yes" : "No")}");
        }
    }

    private void UpdateVeterinarianUI()
{
    Console.WriteLine("\n--- ✏️  Update Veterinarian 👨‍⚕️ ---");

    try
    {
        ViewVeterinariansUI();

        string idInput = Validator.ValidateContent("\nEnter Veterinarian ID: ");
        if (!Guid.TryParse(idInput, out Guid veterinarianId))
        {
            Console.WriteLine("⚠️ Invalid ID format");
            return;
        }

        // Obtener veterinario actual
        var vet = _veterinarianService.GetVeterinarianById(veterinarianId);
        if (vet == null)
        {
            Console.WriteLine("❌ No veterinarian found with that ID");
            return;
        }

        Console.WriteLine($"\nCurrent data for {vet.Name}:");
        Console.WriteLine($"👤 Name: {vet.Name}");
        Console.WriteLine($"🎂 Age: {vet.Age}");
        Console.WriteLine($"🏠 Address: {vet.Address}");
        Console.WriteLine($"📞 Phone: {vet.Phone}");
        Console.WriteLine($"📧 Email: {vet.Email}");
        Console.WriteLine($"🧼 Specialty: {vet.Specialty}");
        Console.WriteLine($"🔢 License: {vet.LicenseNumber}");

        Console.WriteLine("\nUpdate fields (y/n):");

        string name = vet.Name;
        int age = vet.Age;
        string address = vet.Address;
        string phone = vet.Phone;
        string email = vet.Email;
        Specialties specialty = vet.Specialty;
        string license = vet.LicenseNumber;

        if (Validator.AskYesNo("Change name? (y/n): "))
            name = Validator.ValidateContent("👤 Enter new name: ");
        
        if (Validator.AskYesNo("Change age? (y/n): "))
            age = Validator.ValidatePositiveInt("🎂 Enter new age: ");
        
        if (Validator.AskYesNo("Change address? (y/n): "))
            address = Validator.ValidateContent("🏠 Enter new address: ");
        
        if (Validator.AskYesNo("Change phone? (y/n): "))
            phone = Validator.ValidateContent("📞 Enter new phone: ");
        
        if (Validator.AskYesNo("Change email? (y/n): "))
            email = Validator.ValidateContent("📧 Enter new email: ");
        
        if (Validator.AskYesNo("Change specialty? (y/n): "))
        {
            Console.WriteLine("\n🧼 --- Specialties ---");
            foreach (var s in Enum.GetValues(typeof(Specialties)))
                Console.WriteLine($"{(int)s}. {s}");

            int specialtyInt = Validator.ValidatePositiveInt("\nSelect specialty number: ");
            if (Enum.IsDefined(typeof(Specialties), specialtyInt))
                specialty = (Specialties)specialtyInt;
            else
            {
                Console.WriteLine("⚠️ Invalid specialty number. Specialty not changed.");
            }
        }

        
        if (Validator.AskYesNo("Change license? (y/n): "))
            license = Validator.ValidateContent("🔢 Enter new license number: ");

        _veterinarianService.UpdateVeterinarian(
            veterinarianId,
            name,
            age,
            address,
            phone,
            email,
            specialty,
            license
        );

        Console.WriteLine("\n✅ Veterinarian updated successfully!");
    }
    catch (FormatException)
    {
        Console.WriteLine("⚠️ Invalid format. Please enter valid data.");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine("❌ No veterinarian found with that ID.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error updating veterinarian: {ex.Message}");
    }
}


    private void RemoveVeterinarianUI()
    {
        Console.WriteLine("\n--- ❌ Remove Veterinarian ---");

        ViewVeterinariansUI();

        Console.Write("\nEnter Veterinarian ID to remove: ");
        var vetIdInput = Console.ReadLine();

        if (!Guid.TryParse(vetIdInput, out Guid vetId))
        {
            Console.WriteLine("⚠️ Invalid ID format.");
            return;
        }

        try
        {
            _veterinarianService.RemoveVeterinarian(vetId);
            Console.WriteLine("✅ Veterinarian removed successfully!");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("⚠️ Veterinarian not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error removing veterinarian: {ex.Message}");
        }
    }
}
