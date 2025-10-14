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

        if (petAge < 0)
            throw new ArgumentException("Pet age cannot be negative", nameof(petAge));

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

    /// <summary>
    /// Returns a single pet by ID.
    /// </summary>
    public Pet? GetPetById(Guid id)
    {
        return _petRepo.GetById(id);
    }















    // QUERYS

    public IEnumerable<Pet> SortPets(int criteria, int order)
    {
        var pets = _petRepo.GetAll().ToList();
        if (!pets.Any())
            throw new InvalidOperationException("No pets found");

        return criteria switch
        {
            1 => order == 1 ? pets.OrderBy(p => p.Name) : pets.OrderByDescending(p => p.Name),
            2 => order == 1 ? pets.OrderBy(p => p.Age) : pets.OrderByDescending(p => p.Age),
            3 => order == 1 ? pets.OrderBy(p => p.Species) : pets.OrderByDescending(p => p.Species),
            _ => pets
        };
    }

    public IEnumerable<IGrouping<string, Pet>> GroupPetsBySpecies()
    {
        var pets = _petRepo.GetAll();
        if (!pets.Any())
            throw new InvalidOperationException("No pets found");

        return pets.GroupBy(p => p.Species);
    }

    public IEnumerable<Pet> FilterPetsByAge(int minAge)
    {
        var pets = _petRepo.GetAll();
        var filtered = pets.Where(p => p.Age >= minAge).ToList();

        if (!filtered.Any())
            throw new InvalidOperationException("No pets found with the given age criteria");

        return filtered;
    }

    public Pet GetOldestPet()
    {
        var pets = _petRepo.GetAll();
        if (!pets.Any())
            throw new InvalidOperationException("No pets found");

        return pets.OrderByDescending(p => p.Age).First();
    }
}