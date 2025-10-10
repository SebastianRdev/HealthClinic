namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;
using HealthClinic.repositories;

/// <summary>
/// Service that manages the business logic related to pets in the HealthClinic system.
/// Allows you to view, sort, group, and display pets and their owners.
/// </summary>
public class PetService
{
    /// <summary>
    /// Register a new pet interactively
    /// </summary>
    /// <returns>Pet registered</returns>
    public static Pet? RegisterPet()
    {
        Console.WriteLine("\n--- 📝 Register Pet 🐕 ---");

        var customerRepo = new RepositoryDict<Customer>();
        var customers = customerRepo.GetAll();

        if (!Validator.IsExist(customers, "⚠️  No customers available. Please register a customer first")) return null;

        CustomerService.ShowAvailableCustomers(customers);

        Customer? owner = null;
        while (owner == null)
        {
            Console.Write("\nEnter the customer's ID for this pet: ");
            string input = Console.ReadLine()!;

            owner = customers.FirstOrDefault(c => c.Id.ToString().Equals(input, StringComparison.OrdinalIgnoreCase));

            if (!Validator.IsExist(owner, "❌ No customer found with that ID. Try again")) continue;
        }


        string petName = Validator.ValidateContent("\n📛 Enter the pet's name: ");
        string petSpecies = Validator.ValidateContent("\n🐕 Enter the pet's species: ");
        string petBreed = Validator.ValidateContent("\n🐾 Enter the pet's breed (If you don't know, write: unknown): ");
        int petAge = Validator.ValidatePositiveInt("\n🎂 Enter the pet's age: ");

        Pet pet = new Pet(petName, petSpecies, petBreed, petAge)
        {
            Owner = owner
        };

        var petRepo = new Repository<Pet>();
        petRepo.Add(pet);
        pet.Register();

        owner.Pets.Add(pet);

        Console.WriteLine($"\n✅ Pet '{pet.Name}' successfully registered and assigned to {owner.Name}.");

        return pet;
    }


    /// <summary>
    /// Displays the list of pets and their owners on the console.
    /// </summary>
    /// <param name="PetList">List of customers to display</param>
    public static void ViewPets(List<Pet> PetList)
    {
        if (!Validator.IsExist(PetList, "⚠️  No pets found")) return;

        Console.WriteLine("\n--- 🐾 Pets List ---");
        foreach (var pet in PetList)
        {
            Console.WriteLine($"\n🆔 ID: {pet.Id}");
            Console.WriteLine($"🐶 Name: {pet.Name}");
            Console.WriteLine($"📚 Species: {pet.Species}");
            Console.WriteLine($"🐈 Breed: {pet.Breed}");
            Console.WriteLine($"🎂 Age: {pet.Age} años");
            Console.WriteLine($"👤 Owner: {pet.Owner?.Name} (🆔 {pet.Owner?.Id})");
        }
    }

    public static Pet EditPet(Pet pet)
    {
        Console.WriteLine("\n--- 📝 Update Pet 🐕 ---");

        string petName = Validator.ValidateContentEmpty("\n📛 New name (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(petName)) pet.Name = petName;

        string petSpecies = Validator.ValidateContentEmpty("\n🐕 New species (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(petSpecies)) pet.Species = petSpecies;

        string petBreed = Validator.ValidateContentEmpty("\n🐾 New breed (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(petBreed)) pet.Breed = petBreed;

        string petAgeInput = Validator.ValidateContentEmpty("\n🎂 New age (leave empty to keep current): ", allowEmpty: true);
        if (int.TryParse(petAgeInput, out int petAge)) pet.Age = petAge;

        Console.WriteLine($"✅ Pet '{pet.Name}' updated successfully.");
        return pet;
    }

    public static void UpdatedPet(List<Pet> petList)
    {
        Console.WriteLine("\n--- 📝 Update Pet ---");

        ViewPets(petList);

        Console.Write("\nEnter the Pet ID you want to update: ");
        string petIdInput = Console.ReadLine()!.Trim();

        var pet = petList.FirstOrDefault(p => p.Id.ToString() == petIdInput);
        if (!Validator.IsExist(pet, "❌ No pet found with that ID")) return;
        if (pet == null) return;

        Pet updatedPet = EditPet(pet);

        var petRepo = new Repository<Pet>();
        petRepo.Update(updatedPet);

        Console.WriteLine("✅ Pet updated successfully!");
    }

    public static void RemovePet(List<Pet> petList)
    {
        Console.WriteLine("\n--- 📝 Remove Pet ---");

        ViewPets(petList);

        Console.Write("\nEnter the Pet ID you want to remove: ");
        string petIdInput = Console.ReadLine()!.Trim();

        var pet = petList.FirstOrDefault(p => p.Id.ToString() == petIdInput);
        if (!Validator.IsExist(pet, "❌ No pet found with that ID")) return;
        if (pet == null) return;

        Console.WriteLine($"🗑️ Removing pet: {pet.Name} (ID: {pet.Id})");

        // Desvincular al dueño de la mascota
        if (pet.Owner != null)
        {
            Console.WriteLine($"🐾 Disassociating pet: {pet.Name} from owner: {pet.Owner.Name}");
            pet.Owner.Pets.Remove(pet);
            pet.Owner = null;
        }

        petList.Remove(pet);

        Console.WriteLine($"✅ Pet '{pet.Name}' has been successfully removed.");
    }




































    // QUERIES
    /// <summary>
    /// Allows the user to sort the list of pets by name, age, or species, in ascending or descending order.
    /// </summary>
    /// <param name="PetList">List of pets to be sorted</param>
    public static void MainSort(List<Pet> PetList)
    {
        int order;
        int criteria;

        while (true)
        {
            try
            {
                Console.WriteLine("Would you like to sort by?: \n1. Name \n2. Age \n3. Species");
                criteria = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(criteria)) continue;
                break;
            }
            catch
            {
                Console.WriteLine("❌ Invalid input. Please enter a number");
                continue;
            }
        }

        while (true)
        {
            try
            {
                Console.WriteLine("\nIn what order?: \n1. Ascending \n2. Descending");
                order = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(order)) continue;
                break;
            }
            catch
            {
                Console.WriteLine("❌ Invalid input. Please enter a number");
                continue;
            }
        }

        switch (criteria)
        {
            case 1:
                SortByName(PetList, order);
                break;
            case 2:
                SortByAge(PetList, order);
                break;
            case 3:
                SortBySpecies(PetList, order);
                break;
            default:
                Console.WriteLine("⚠️  Invalid choice. Please try again");
                break;
        }
    }

    /// <summary>
    /// Sort the list of pets by name and display the result.
    /// </summary>
    /// <param name="PetList">List of pets</param>
    /// <param name="order">1 for ascending, 2 for descending</param>
    public static void SortByName(List<Pet> PetList, int order)
    {
        Console.WriteLine("\n--- 🐾 Pets Sorted by Name ---");
        Console.WriteLine("----------------------------------------------------");
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Name).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Name).ToList();
        ViewPets(sortedPets);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Sort the list of pets by age and display the result.
    /// </summary>
    /// <param name="PetList">List of pets</param>
    /// <param name="order">1 for ascending, 2 for descending</param>
    public static void SortByAge(List<Pet> PetList, int order)
    {
        Console.WriteLine("\n--- 🐾 Pets Sorted by Age ---");
        Console.WriteLine("----------------------------------------------------");
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Age).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Age).ToList();
        ViewPets(sortedPets);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Sort the list of pets by species and display the result.
    /// </summary>
    /// <param name="PetList">List of pets</param>
    /// <param name="order">1 for ascending, 2 for descending</param>
    public static void SortBySpecies(List<Pet> PetList, int order)
    {
        Console.WriteLine("\n--- 🐾 Pets Sorted by Species ---");
        Console.WriteLine("----------------------------------------------------");
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Species).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Species).ToList();
        ViewPets(sortedPets);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Group pets by species and display the result.
    /// </summary>
    /// <param name="PetList">List of pets</param>
    public static void GroupPetsBySpecies(List<Pet> PetList)
    {
        if (!Validator.IsExist(PetList, "⚠️  No pets found")) return;

        var groupedPets = PetList
            .GroupBy(p => p.Species ?? "Unknown")
            .OrderBy(g => g.Key);

        ShowGroupPetsBySpecies(groupedPets);
    }

    /// <summary>
    /// Muestra el resultado de agrupar mascotas por especie.
    /// </summary>
    /// <param name="groupedPets">Colección agrupada por especie.</param>
    public static void ShowGroupPetsBySpecies(IEnumerable<IGrouping<string, Pet>> groupedPets)
    {
        Console.WriteLine("\n--- 🐾 Pets Grouped by Species ---");
        Console.WriteLine("----------------------------------------------------");

        foreach (var group in groupedPets)
        {
            Console.WriteLine($"\n🐾 Species: {group.Key} ({group.Count()} pets)");

            foreach (var pet in group)
            {
                Console.WriteLine($"   📛 {pet.Name} - Breed: {pet.Breed}, Age: {pet.Age}");
                Console.WriteLine($"      👤 Owner: {pet.Owner?.Name ?? "No owner"}");
            }
        }
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Combined query showing customers with 3-year-old dogs.
    /// </summary>
    /// <param name="CustomerList">List of customers</param>
    public static void CombinedConsultation(List<Customer> CustomerList)
    {
        var result = CustomerList
            .Where(c => c.Pets.Any(p => p.Species != null && p.Species.Equals("dog", StringComparison.OrdinalIgnoreCase) && p.Age == 3))
            .Select(c => new { c.Name, c.Phone })
            .ToList();

        if (!Validator.IsExist(result, "⚠️  No customers found with a 3-year-old dog")) return;

        ShowCombinedConsultation(result);
    }

    /// <summary>
    /// Shows the result of the combined query for customers with 3-year-old dogs.
    /// </summary>
    /// <param name="result">Collection of results</param>
    public static void ShowCombinedConsultation(IEnumerable<dynamic> result)
    {
        Console.WriteLine("\n--- 🐶 Clients with a 3-year-old dog ---");
        Console.WriteLine("----------------------------------------------------");
        foreach (var entry in result)
        {
            Console.WriteLine($"👤 Name: {entry.Name}, 📞 Phone: {entry.Phone}");
        }
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Shows the number of pets by species.
    /// </summary>
    /// <param name="PetList">List of pets</param>
    public static void PetsOfEachSpecies(List<Pet> PetList)
    {
        var speciesCount = PetList.GroupBy(p => p.Species)
            .Select(g => new { Species = g.Key, Count = g.Count() }).ToList();

        ShowPetsOfEachSpecies(speciesCount);
    }

    /// <summary>
    /// Shows the result of the query for the number of pets by species.
    /// </summary>
    /// <param name="speciesCount">Collection of species and quantities</param>
    public static void ShowPetsOfEachSpecies(IEnumerable<dynamic> speciesCount)
    {
        Console.WriteLine("\n--- 🐾 Pets of Each Species ---");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine($"\n🐾 Total different species of pets: {speciesCount.Count()}");
        foreach (var species in speciesCount)
        {
            Console.WriteLine($"Species: {species.Species}, Number of pets: {species.Count}");
        }
        Console.WriteLine("\n----------------------------------------------------");
    }
}
