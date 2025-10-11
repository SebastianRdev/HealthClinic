namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.repositories;
using HealthClinic.utils;
using HealthClinic.models.Enums;

public class VeterinarianService
{
    /// <summary>
    /// Registers a new veterinarian interactively.
    /// </summary>
    public static Veterinarian? RegisterVeterinarian()
    {
        Console.WriteLine("\n--- 📝 Register Veterinarian 🩺 ---");

        string name = Validator.ValidateContent("\n👤 Veterinarian name: ");
        int age = Validator.ValidatePositiveInt("\n🎂 Age: ");
        string address = Validator.ValidateContent("\n🏠 Address: ");
        string phone = Validator.ValidateContent("\n📞 Phone: ");
        string email = Validator.ValidateContent("\n📧 Email: ");
        string licenseNumber = Validator.ValidateContent("\n🔢 License number: ");

        Console.WriteLine("\nSpecialty:");
        foreach (var value in Enum.GetValues(typeof(Specialties)))
        {
            Console.WriteLine($"{(int)value}. {value}");
        }
        int specialtyInt = Validator.ValidatePositiveInt("\nSelect specialty number: ");
        Specialties specialty = Enum.IsDefined(typeof(Specialties), specialtyInt) ? (Specialties)specialtyInt : Specialties.General;

        Veterinarian vet = new Veterinarian(name, age, address, phone, email, licenseNumber, specialty);

        var vetRepo = new Repository<Veterinarian>();
        vetRepo.Add(vet);

        vet.Register();
        return vet;
    }

    /// <summary>
    /// Shows the list of veterinarians.
    /// </summary>
    public static void ViewVeterinarians(List<Veterinarian> vetList)
    {
        if (!Validator.IsExist(vetList, "⚠️  No veterinarians registered")) return;

        Console.WriteLine("\n--- 🩺 Veterinarian List ---");
        foreach (var vet in vetList)
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

    /// <summary>
    /// Edits veterinarian data.
    /// </summary>
    public static Veterinarian EditVeterinarian(Veterinarian vet)
    {
        Console.WriteLine("\n--- 📝 Update Veterinarian ---");

        string name = Validator.ValidateContentEmpty("\n👤 New name (leave empty to keep): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(name)) vet.Name = name;

        string ageInput = Validator.ValidateContentEmpty("\n🎂 New age (leave empty to keep): ", allowEmpty: true);
        if (int.TryParse(ageInput, out int age)) vet.Age = age;

        string address = Validator.ValidateContentEmpty("\n🏠 New address (leave empty to keep): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(address)) vet.Address = address;

        string phone = Validator.ValidateContentEmpty("\n📞 New phone (leave empty to keep): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(phone)) vet.Phone = phone;

        string email = Validator.ValidateContentEmpty("\n📧 New email (leave empty to keep): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(email)) vet.Email = email;

        Console.WriteLine("\nDo you want to change the specialty? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            foreach (var value in Enum.GetValues(typeof(Specialties)))
            {
                Console.WriteLine($"{(int)value}. {value}");
            }
            int specialtyInt = Validator.ValidatePositiveInt("\nSelect specialty number: ");
            if (Enum.IsDefined(typeof(Specialties), specialtyInt))
                vet.Specialty = (Specialties)specialtyInt;
        }
        return vet;
    }

    /// <summary>
    /// Updates a veterinarian in the list.
    /// </summary>
    public static void UpdateVeterinarian(List<Veterinarian> vetList)
    {
        Console.WriteLine("\n--- 📝 Update Veterinarian ---");
        ViewVeterinarians(vetList);

        Console.Write("\nEnter the ID of the veterinarian to update: ");
        string vetIdInput = Console.ReadLine()!.Trim();

        var vet = vetList.FirstOrDefault(v => v.Id.ToString() == vetIdInput);
        if (!Validator.IsExist(vet, "❌ No veterinarian found with that ID")) return;
        if (vet == null) return;

        Veterinarian updatedVet = EditVeterinarian(vet);

        var vetRepo = new Repository<Veterinarian>();
        vetRepo.Update(updatedVet);

        Console.WriteLine("✅ Veterinarian updated successfully!");
    }

    /// <summary>
    /// Removes a veterinarian from the list.
    /// </summary>
    public static void RemoveVeterinarian(List<Veterinarian> vetList)
    {
        Console.WriteLine("\n--- 💤 Deactivate Veterinarian ---");
        ViewVeterinarians(vetList);

        Console.Write("\nEnter the ID of the veterinarian to deactivate: ");
        string vetIdInput = Console.ReadLine()!.Trim();

        var vet = vetList.FirstOrDefault(v => v.Id.ToString() == vetIdInput);
        if (!Validator.IsExist(vet, "❌ No veterinarian found with that ID")) return;
        if (vet == null) return;

        if (!vet.IsActive)
        {
            Console.WriteLine($"⚠️ Veterinarian '{vet.Name}' is already inactive");
            return;
        }

        Console.WriteLine($"😴 Deactivating veterinarian: {vet.Name} (ID: {vet.Id})");

        vet.IsActive = false;

        Console.WriteLine($"✅ Veterinarian '{vet.Name}' has been marked as inactive");
    }
}