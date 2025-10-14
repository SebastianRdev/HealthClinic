namespace HealthClinic.menus;

using HealthClinic.services;
using HealthClinic.utils;

public class PetMenu
{
    private readonly PetService _petService;

    public PetMenu(PetService petService)
    {
        _petService = petService;
    }

    public void PetMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowPetMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PetCRUD();
                        continue;
                    case 2:
                        Console.WriteLine("\nBack to main menu 🐶🐱");
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
    public void PetCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowPetCRUD();
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
                        RegisterPetUI();
                        continue;
                    case 2:
                        ViewPetsUI();
                        continue;
                    case 3:
                        UpdatePetUI();
                        continue;
                    case 4:
                        RemovePetUI();
                        continue;
                    case 5:
                        Console.WriteLine("\n Back to Pet main menu 🐶🐱");
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

    private void RegisterPetUI()
    {
        Console.WriteLine("\n--- 📝 Register Pet 🐕 ---");

        try
        {
            // Mostrar clientes disponibles
            var customers = _petService.GetAllCustomers();
            if (customers.Count == 0)
            {
                Console.WriteLine("⚠️ No customers available. Please register one first");
                return;
            }

            Console.WriteLine("\n--- 👥 Customer List ---");
            foreach (var c in customers)
                Console.WriteLine($"🆔 {c.Id} | 👤 {c.Name} | 📞 {c.Phone}");

            string customerInput = Validator.ValidateContent("\nEnter Customer ID: ");
            if (!Guid.TryParse(customerInput, out Guid customerId))
            {
                Console.WriteLine("⚠️ Invalid ID format. Please enter a valid GUID.");
                return;
            }

            string petName = Validator.ValidateContent("\n📛 Enter pet's name: ");
            int petAge = Validator.ValidatePositiveInt("🎂 Enter pet's age: ");
            string petSpecies = Validator.ValidateContent("🐕 Enter pet's species: ");
            string petBreed = Validator.ValidateContent("🐾 Enter pet's breed (or 'unknown' if not sure): ");

            var pet = _petService.RegisterPet(customerId, petName, petAge, petSpecies, petBreed);
            Console.WriteLine($"\n✅ Pet registered successfully with ID: {pet.Id}");
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


    private void ViewPetsUI()
    {
        var pets = _petService.ViewPets().ToList();
        if (pets.Count == 0)
        {
            Console.WriteLine("⚠️  No pets registered");
            return;
        }

        foreach (var pet in pets)
        {
            Console.WriteLine($"\n🆔 ID: {pet.Id}");
            Console.WriteLine($"🐶 Name: {pet.Name}");
            Console.WriteLine($"📚 Species: {pet.Species}");
            Console.WriteLine($"🐈 Breed: {pet.Breed}");
            Console.WriteLine($"🎂 Age: {pet.Age} años");
            Console.WriteLine($"👤 Owner: {pet.Owner?.Name} (🆔 {pet.Owner?.Id})");
        }
    }

    private void UpdatePetUI()
    {
        Console.WriteLine("\n--- ✏️  Update Pet ---");
        try
        {
            ViewPetsUI();

            Console.Write("\nEnter Pet ID: ");
            var idInput = Console.ReadLine();

            if (!Guid.TryParse(idInput, out Guid petId))
            {
                Console.WriteLine("⚠️  Invalid ID format");
                return;
            }

            // Obtener la mascota actual
            var pet = _petService.GetPetById(petId);
            if (pet == null)
            {
                Console.WriteLine("❌ No pet found with that ID");
                return;
            }

            Console.WriteLine($"\nCurrent data for {pet.Name}:");
            Console.WriteLine($"👤 Owner: {pet.Owner?.Name ?? "No owner"}");
            Console.WriteLine($"🐾 Name: {pet.Name}");
            Console.WriteLine($"🎂 Age: {pet.Age}");
            Console.WriteLine($"🧬 Species: {pet.Species}");
            Console.WriteLine($"🐕 Breed: {pet.Breed}");

            Console.WriteLine("\nUpdate fields (y/n):");

            Guid? newCustomerId = pet.Owner?.Id;
            string petName = pet.Name;
            int petAge = pet.Age;
            string petSpecies = pet.Species;
            string petBreed = pet.Breed;

            if (Validator.AskYesNo("Change owner? (y/n): "))
            {
                var customers = _petService.GetAllCustomers();
                if (customers.Count == 0)
                {
                    Console.WriteLine("⚠️ No customers available. Please register one first");
                    return;
                }

                Console.WriteLine("\n--- 👥 Available Customers ---");
                foreach (var c in customers)
                {
                    Console.WriteLine($"🆔 {c.Id} | 👤 {c.Name} | 📞 {c.Phone}");
                }

                Console.Write("\nEnter new Customer ID: ");
                var customerInput = Console.ReadLine();
                if (Guid.TryParse(customerInput, out Guid customerId))
                    newCustomerId = customerId;
                else
                    Console.WriteLine("⚠️ Invalid Customer ID format. Owner not changed.");
            }

            if (Validator.AskYesNo("Change name? (y/n): "))
                petName = Validator.ValidateContent("🐾 Enter new name: ");

            if (Validator.AskYesNo("Change age? (y/n): "))
                petAge = Validator.ValidatePositiveInt("🎂 Enter new age: ");
            
            if (Validator.AskYesNo("Change species? (y/n): "))
                petSpecies = Validator.ValidateContent("🧬 Enter new species: ");
            
            if (Validator.AskYesNo("Change breed? (y/n): "))
                petBreed = Validator.ValidateContent("🐕 Enter new breed: ");

            _petService.UpdatePet(
                petId,
                newCustomerId,
                petName,
                petAge,
                petSpecies,
                petBreed
            );

            Console.WriteLine("\n✅ Pet updated successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("⚠️ Invalid ID format. Please enter a valid value");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("❌ No pet found with that ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error updating pet: {ex.Message}");
        }
    }


    private void RemovePetUI()
    {
        Console.WriteLine("\n--- ❌ Remove Pet ---");

        ViewPetsUI();

        Console.Write("\nEnter Pet ID to remove: ");
        var petIdInput = Console.ReadLine();

        if (!Guid.TryParse(petIdInput, out Guid petId))
        {
            Console.WriteLine("⚠️ Invalid ID format.");
            return;
        }

        try
        {
            _petService.RemovePet(petId);
            Console.WriteLine("✅ Pet removed successfully!");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("⚠️ Pet not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error removing pet: {ex.Message}");
        }
    }
}
