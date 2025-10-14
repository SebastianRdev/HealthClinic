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
        try
        {
            Console.WriteLine("\n--- 📝 Register Veterinarian ---");

            Console.Write("\n👤 Name: ");
            string vetName = Console.ReadLine()!.Trim();

            Console.Write("\n🎂 Age: ");
            int vetAge = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("\n🏠 Address: ");
            string vetAddress = Console.ReadLine()!.Trim();

            Console.Write("\n📞 Phone: ");
            string vetPhone = Console.ReadLine()!.Trim();

            Console.Write("\n📧 Email: ");
            string vetEmail = Console.ReadLine()!.Trim();

            Console.WriteLine("\n🧼 --- Specialties ---");
            foreach (var specialty in Enum.GetValues(typeof(Specialties)))
                Console.WriteLine($"{(int)specialty}. {specialty}");

            Console.Write("\nEnter specialty (number): ");
            int specialtyInt = int.Parse(Console.ReadLine()!.Trim());
            var specialtyType = (Specialties)specialtyInt;

            Console.Write("\n🔢 License number: ");
            string vetLicense = Console.ReadLine()!.Trim();

            // Register veterinarian
            var veterinarian = _veterinarianService.RegisterVeterinarian(vetName, vetAge, vetAddress, vetPhone, vetEmail, specialtyType, vetLicense);
            Console.WriteLine($"\n✅ Veterinarian registered successfully with ID: {veterinarian.Id}");
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
        Console.WriteLine("\n--- ✏️  Update Veterinarian ---");

        ViewVeterinariansUI();

        Console.Write("\nEnter Veterinarian ID: ");
        var idInput = Console.ReadLine();

        if (!Guid.TryParse(idInput, out Guid veterinarianId))
        {
            Console.WriteLine("⚠️  Invalid ID format");
            return;
        }

        string? newVetName = null;
        int? newVetAge = null;
        string? newVetAddress = null;
        string? newVetPhone = null;
        string? newVetEmail = null;
        Specialties? newSpecialty = null;
        string? newVetLicense = null;

        // Update Name
        Console.Write("Update Name? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new name: ");
            newVetName = Console.ReadLine();
        }

        // Update Age
        Console.Write("Update Age? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new age: ");
            var ageInput = Console.ReadLine();
            if (int.TryParse(ageInput, out int age))
                newVetAge = age;
            else
                Console.WriteLine("⚠️  Invalid age format. Age not updated");
        }

        // Update Address
        Console.Write("Update Address? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new address: ");
            newVetAddress = Console.ReadLine();
        }

        // Update Phone
        Console.Write("Update Phone? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new phone: ");
            newVetPhone = Console.ReadLine();
        }

        // Update Email
        Console.Write("Update Email? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new email: ");
            newVetEmail = Console.ReadLine();
        }

        // Update specialty
        Console.Write("Update Specialty? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.WriteLine("\nSpecialties:");
            foreach (var s in Enum.GetValues(typeof(Specialties)))
                Console.WriteLine($"{(int)s}. {s}");

            Console.Write("Select specialty number: ");
            if (int.TryParse(Console.ReadLine(), out int specialtyInt) &&
                Enum.IsDefined(typeof(Specialties), specialtyInt))
                newSpecialty = (Specialties)specialtyInt;
            else
                Console.WriteLine("⚠️  Invalid specialty number");
        }

        // Update License
        Console.Write("Update License? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new license: ");
            newVetLicense = Console.ReadLine();
        }

        try
        {
            _veterinarianService.UpdateVeterinarian(
                veterinarianId,
                newVetName,
                newVetAge,
                newVetAddress,
                newVetPhone,
                newVetEmail,
                newSpecialty,
                newVetLicense
            );

            Console.WriteLine("\n✅ Veterinarian updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
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
