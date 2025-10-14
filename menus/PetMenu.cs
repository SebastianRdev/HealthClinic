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
                        UpdatedPetUI();
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
        try
        {
            Console.WriteLine("\n--- 📝 Register Pet 🐕 ---");

            // Show all customers
            var customers = _petService.GetAllCustomers();
            if (customers.Count == 0)
            {
                Console.WriteLine("⚠️  No customers available. Please register one first");
                return;
            }

            Console.WriteLine("\n --- Customer List ---");
            foreach (var c in customers)
                Console.WriteLine($"ID: {c.Id} | Name: {c.Name}");

            Console.Write("\nEnter customer ID: ");
            Guid customerId = Guid.Parse(Console.ReadLine()!.Trim());

            Console.Write("\n📛 Name: ");
            string petName = Console.ReadLine()!.Trim();

            Console.Write("\n🎂 Age: ");
            int petAge = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("\n🐕 Species: ");
            string petSpecies = Console.ReadLine()!.Trim();

            Console.Write("\n🐾 Breed (If you don't know, write: unknown): ");
            string petBreed = Console.ReadLine()!.Trim();

            
            // // 🧼 Mostrar servicios disponibles
            // Console.WriteLine("\n🧼 --- Available Services ---");
            // foreach (var service in Enum.GetValues(typeof(ServiceType)))
            //     Console.WriteLine($"{(int)service}. {service}");

            // Console.Write("\nEnter service type (number): ");
            // int serviceInt = int.Parse(Console.ReadLine()!.Trim());
            // var serviceType = (ServiceType)serviceInt;

            // Register pet
            var pet = _petService.RegisterPet(customerId, petName, petAge, petSpecies, petBreed);
            Console.WriteLine($"\n✅ Pet registered successfully with ID: {pet.Id}");
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

    private void UpdatedPetUI()
    {
        Console.WriteLine("\n--- ✏️  Update Pet ---");

        ViewPetsUI();

        Console.Write("\nEnter Pet ID: ");
        var idInput = Console.ReadLine();

        if (!Guid.TryParse(idInput, out Guid petId))
        {
            Console.WriteLine("⚠️  Invalid ID format");
            return;
        }

        Guid? newCustomerId = null;
        string? newPetName = null;
        int? newPetAge = null;
        string? newPetSpecies = null;
        string? newPetBreed = null;

        // Update customer
        Console.Write("Update Customer? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            // Show all customers
            var customers = _petService.GetAllCustomers();
            if (customers.Count == 0)
            {
                Console.WriteLine("⚠️ No customers available. Please register one first");
                return;
            }

            Console.Write("Enter new Customer ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid customerId))
                newCustomerId = customerId;
            else
                Console.WriteLine("⚠️  Invalid Customer ID format");
        }

        // Update Name
        Console.Write("Update Name? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new name: ");
            newPetName = Console.ReadLine();
        }

        // Update Age
        Console.Write("Update Age? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new age: ");
            var ageInput = Console.ReadLine();
            if (int.TryParse(ageInput, out int age))
                newPetAge = age;
            else
                Console.WriteLine("⚠️  Invalid age format. Age not updated");
        }

        // Update Species
        Console.Write("Update Species? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new species: ");
            newPetSpecies = Console.ReadLine();
        }

        // Update Breed
        Console.Write("Update Breed? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            Console.Write("Enter new breed: ");
            newPetBreed = Console.ReadLine();
        }

        try
        {
            _petService.UpdatePet(
                petId,
                newCustomerId,
                newPetName,
                newPetAge,
                newPetSpecies,
                newPetBreed
            );

            Console.WriteLine("\n✅ Pet updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
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
