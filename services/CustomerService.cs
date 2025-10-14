namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;
using HealthClinic.repositories;

/// <summary>
/// Service that manages customer-related business logic in the HealthClinic system.
/// Handles registration, updates, deletion, and viewing of customers and their pets.
/// </summary>
public class CustomerService  // AL ELIMINAR UN PET NO SE ELIMINA DEL DUEÑO
{
    private readonly IRepository<Customer> _customerRepo;
    private readonly IRepository<Pet> _petRepo;

    public CustomerService(
        IRepository<Customer> customerRepo,
        IRepository<Pet> petRepo)
    {
        _customerRepo = customerRepo;
        _petRepo = petRepo;
    }

    /// <summary>
    /// Registers a new customer with optional pets.
    /// </summary>
    public Customer RegisterCustomer(string name, int age, string address, string phone, List<Pet>? pets = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address is required", nameof(address));
        if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone is required", nameof(phone));
        if (age <= 0) throw new ArgumentException("Age must be positive", nameof(age));

        pets ??= new List<Pet>();

        var customer = new Customer(name, age, address, phone, pets);

        // Associate pets
        foreach (var pet in pets)
        {
            pet.Owner = customer;
            _petRepo.Add(pet);
        }

        _customerRepo.Add(customer);
        return customer;
    }

    /// <summary>
    /// Returns all customers.
    /// </summary>
    public List<Customer> ViewCustomers()
    {
        return _customerRepo.GetAll();
    }

    /// <summary>
    /// Updates a customer's data.
    /// </summary>
    public void UpdateCustomer(Guid customerId, string? name = null, int? age = null, string? address = null, string? phone = null)
    {
        var customer = _customerRepo.GetById(customerId) ?? throw new KeyNotFoundException("Customer not found");

        if (!string.IsNullOrWhiteSpace(name)) customer.Name = name;
        if (age.HasValue && age.Value > 0) customer.Age = age.Value;
        if (!string.IsNullOrWhiteSpace(address)) customer.Address = address;
        if (!string.IsNullOrWhiteSpace(phone)) customer.Phone = phone;

        _customerRepo.Update(customer);
    }

    /// <summary>
    /// Removes a customer and disassociates their pets.
    /// </summary>
    public void RemoveCustomer(Guid customerId)
    {
        var customer = _customerRepo.GetById(customerId) ?? throw new KeyNotFoundException("Customer not found");

        foreach (var pet in customer.Pets)
        {
            pet.Owner = null;
        }

        _customerRepo.Remove(customerId);
    }

    /// <summary>
    /// Returns a single customer by ID.
    /// </summary>
    public Customer? GetCustomerById(Guid id)
    {
        return _customerRepo.GetById(id);
    }

    /// <summary>
    /// Filters customers who have pets of a specific name, age, address.
    /// </summary>
    public IEnumerable<Customer> SortCustomers(int criteria, int order)
    {
        var customers = _customerRepo.GetAll().ToList();
        if (!customers.Any())
            throw new InvalidOperationException("No customers found");

        return criteria switch
        {
            1 => order == 1 ? customers.OrderBy(c => c.Name) : customers.OrderByDescending(c => c.Name),
            2 => order == 1 ? customers.OrderBy(c => c.Age) : customers.OrderByDescending(c => c.Age),
            3 => order == 1 ? customers.OrderBy(c => c.Address) : customers.OrderByDescending(c => c.Address),
            _ => customers
        };
    }

    public IEnumerable<IGrouping<string, Customer>> GroupCustomersByAddress()
    {
        var customers = _customerRepo.GetAll();
        if (!customers.Any())
            throw new InvalidOperationException("No customers found");

        return customers.GroupBy(c => c.Address);
    }

    public IEnumerable<Customer> FilterCustomersByAge(int minAge)
    {
        var customers = _customerRepo.GetAll();
        var filtered = customers.Where(c => c.Age >= minAge).ToList();

        if (!filtered.Any())
            throw new InvalidOperationException("No customers found with the given age criteria");

        return filtered;
    }

    public Customer GetOldestCustomer()
    {
        var customers = _customerRepo.GetAll();
        if (!customers.Any())
            throw new InvalidOperationException("No customers found");

        return customers.OrderByDescending(c => c.Age).First();
    }
}
