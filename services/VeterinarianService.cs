namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.repositories;
using HealthClinic.utils;
using HealthClinic.models.Enums;

public class VeterinarianService
{
    private readonly IRepository<Veterinarian> _veterinarianRepo;

    public VeterinarianService(IRepository<Veterinarian> veterinarianRepo)
    {
        _veterinarianRepo = veterinarianRepo;
    }

    /// <summary>
    /// Registers a new veterinarian interactively.
    /// </summary>
    public Veterinarian RegisterVeterinarian(string name, int age, string address, string phone, string email, Specialties specialty, string license)
    {
        // Validations
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (age < 0)
            throw new ArgumentException("Veterinarian age cannot be negative", nameof(age));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required", nameof(address));

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required", nameof(phone));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        if (string.IsNullOrWhiteSpace(license))
            throw new ArgumentException("License is required", nameof(license));

        // New veterinarian
        var veterinarian = new Veterinarian(name, age, address, phone, email, license, specialty);

        _veterinarianRepo.Add(veterinarian);
        return veterinarian;
    }

    /// <summary>
    /// Shows the list of veterinarians.
    /// </summary>
    public List<Veterinarian> ViewVeterinarians()
    {
        return _veterinarianRepo.GetAll().ToList();
    }

    /// <summary>
    /// Updates a veterinarian in the list.
    /// </summary>
    public void UpdateVeterinarian(
        Guid veterinarianId,
        string? newVetName = null,
        int? newVetAge = null,
        string? newVetAddress = null,
        string? newVetPhone = null,
        string? newVetEmail = null,
        Specialties? newSpecialty = null,
        string? newVetLicense = null)
    {
        var veterinarian = _veterinarianRepo.GetById(veterinarianId)
            ?? throw new KeyNotFoundException("Veterinarian not found");
        
        if (!string.IsNullOrWhiteSpace(newVetName))
            veterinarian.Name = newVetName;

        if (newVetAge.HasValue)
        {
            if (newVetAge.Value < 0)
                throw new ArgumentException("Veterinarian age cannot be negative");
            veterinarian.Age = newVetAge.Value;
        }

        if (!string.IsNullOrWhiteSpace(newVetAddress))
            veterinarian.Address = newVetAddress;

        if (!string.IsNullOrWhiteSpace(newVetPhone))
            veterinarian.Phone = newVetPhone;

        if (!string.IsNullOrWhiteSpace(newVetEmail))
            veterinarian.Email = newVetEmail;

        if (newSpecialty.HasValue)
            veterinarian.Specialty = newSpecialty.Value;

        if (!string.IsNullOrWhiteSpace(newVetLicense))
            veterinarian.LicenseNumber = newVetLicense;

        _veterinarianRepo.Update(veterinarian);
    }

    /// <summary>
    /// Removes a veterinarian from the list.
    /// </summary>
    public void RemoveVeterinarian(Guid veterinarianId)
    {
        var veterinarian = _veterinarianRepo.GetById(veterinarianId)
            ?? throw new KeyNotFoundException("Veterinarian not found");

        _veterinarianRepo.Remove(veterinarian.Id);
    }
}