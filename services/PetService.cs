namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;
using HealthClinic.repositories;
using HealthClinic.interfaces;

/// <summary>
/// Service that manages the business logic related to pets in the HealthClinic system.
/// Allows you to view, sort, group, and display pets and their owners.
/// </summary>
public class PetService
{
    private readonly IRepository<Pet> _petRepo;
    private readonly IRepository<Customer> _customerRepo;

    public PetService(
        IRepository<Pet> petRepo,
        IRepository<Customer> customerRepo)
    {
        _petRepo = petRepo;
        _customerRepo = customerRepo;
    }

    /// <summary>
    /// Register a new pet interactively
    /// </summary>
    /// <returns>Pet registered</returns>
    public Pet? RegisterPet(Guid customerId, string petName, int petAge, string petSpecies, string petBreed)
    {
        // Validations
        var customer = _customerRepo.GetById(customerId) ?? throw new KeyNotFoundException("Customer not found");

        if (string.IsNullOrWhiteSpace(petName))
            throw new ArgumentException("Pet name is required", nameof(petName));

        if (string.IsNullOrWhiteSpace(petSpecies))
            throw new ArgumentException("Pet species is required", nameof(petSpecies));
        
        if (string.IsNullOrWhiteSpace(petBreed))
            throw new ArgumentException("Pet breed is required", nameof(petBreed));

        // New pet
        var pet = new Pet(petName, petBreed, petSpecies, petAge)
        {
            Owner = customer
        };

        _petRepo.Add(pet);
        customer.Pets.Add(pet);

        return pet;
    }


    /// <summary>
    /// Displays the list of pets and their owners on the console.
    /// </summary>
    /// <returns>A list of all pets.</returns>
    public List<Pet> ViewPets()
    {
        return _petRepo.GetAll().ToList();
    }

    /// <summary>
    /// Edits the information of an existing pet.
    /// </summary>
    public void UpdatePet(
        Guid petId,
        Guid? newCustomerId = null,
        string? newPetName = null,
        int? newPetAge = null,
        string? newPetSpecies = null,
        string? newPetBreed = null)
    {
        var pet = _petRepo.GetById(petId)
            ?? throw new KeyNotFoundException("Pet not found");

        if (newCustomerId.HasValue)
        {
            var customer = _customerRepo.GetById(newCustomerId.Value)
                ?? throw new KeyNotFoundException("Customer not found");
            pet.Owner = customer;
        }

        if (!string.IsNullOrWhiteSpace(newPetName))
            pet.Name = newPetName;

        if (newPetAge.HasValue)
        {
            if (newPetAge.Value < 0)
                throw new ArgumentException("Pet age cannot be negative");
            pet.Age = newPetAge.Value;
        }

        if (!string.IsNullOrWhiteSpace(newPetSpecies))
            pet.Species = newPetSpecies;

        if (!string.IsNullOrWhiteSpace(newPetBreed))
            pet.Breed = newPetBreed;

        _petRepo.Update(pet);
    }

    /// <summary>
    /// Removes a pet from the system, disassociating it from its owner if necessary
    /// </summary>
    public void RemovePet(Guid petId)
    {
        var pet = _petRepo.GetById(petId)
            ?? throw new KeyNotFoundException("Pet not found");

        _petRepo.Remove(pet.Id);
    }

    /// <summary>
    /// Gets all customers from the repository.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    public List<Customer> GetAllCustomers()
    {
        return _customerRepo.GetAll().ToList();
    }




















    // QUERIES
    /// <summary>
    /// Allows the user to sort the list of pets by name, age, or species, in ascending or descending order.
    /// </summary>
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
        // ViewPets(sortedPets);
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
        // ViewPets(sortedPets);
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
        // ViewPets(sortedPets);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Group pets by species and display the result.
    /// </summary>
    public static void GroupPetsBySpecies(List<Pet> PetList)
    {
        if (!Validator.IsExist(PetList, "⚠️  No pets found")) return;

        var groupedPets = PetList
            .GroupBy(p => p.Species ?? "Unknown")
            .OrderBy(g => g.Key);

        ShowGroupPetsBySpecies(groupedPets);
    }

    /// <summary>
    /// Shows the result of grouping pets by species.
    /// </summary>
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
    public static void PetsOfEachSpecies(List<Pet> PetList)
    {
        var speciesCount = PetList.GroupBy(p => p.Species)
            .Select(g => new { Species = g.Key, Count = g.Count() }).ToList();

        ShowPetsOfEachSpecies(speciesCount);
    }

    /// <summary>
    /// Shows the result of the query for the number of pets by species.
    /// </summary>
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
