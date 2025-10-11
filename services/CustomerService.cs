namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;
using HealthClinic.repositories;

/// <summary>
/// Service that manages customer-related business logic in the HealthClinic system.
/// Allows you to register, consult, filter, and display customers and their pets.
/// </summary>
public class CustomerService
{

    /// <summary>
    /// Orchestrate the process of registering a new customer, displaying the menu, and saving the information.
    /// </summary>
    public static void MainRegisterCustomer()
    {
        Console.WriteLine("\n--- 📝 Register Customer ---");

        Customer newCustomer = RegisterCustomerMenu();

        RegisterCustomer(newCustomer);

        newCustomer.Register();
    }

    /// <summary>
    /// Display the interactive menu to register a customer and their pets, validating each piece of information entered.
    /// </summary>
    /// <returns>Registered customer with their pets</returns>
    public static Customer RegisterCustomerMenu()
    {

        string name = Validator.ValidateContent("\n👤 Enter the customer's name: ");
        int age = Validator.ValidatePositiveInt("\n🎂 Enter the customer's age: ");
        string address = Validator.ValidateContent("\n🏠 Enter the customer's address: ");
        string phone = Validator.ValidateContent("\n📞 Enter the customer's phone: ");

        List<Pet> customerPets = new List<Pet>();

        Console.WriteLine("\n--- 📝🐕 Register Pet ---");
        do
        {
            string petName = Validator.ValidateContent("\n📛 Enter the pet's name: ");
            string petSpecies = Validator.ValidateContent("\n🐕 Enter the pet's species: ");
            string petBreed = Validator.ValidateContent("\n🐾 Enter the pet's breed(If you don't know, write: unknown): ");
            int petAge = Validator.ValidatePositiveInt("\n🎂 Enter the pet's age: ");

            Pet pet = new Pet(petName, petSpecies, petBreed, petAge);
            customerPets.Add(pet);
            new Repository<Pet>().Add(pet);
            pet.Register();

            Console.Write("\nDo you want to add another pet? (y/n): ");
            string response = Console.ReadLine()!.Trim().ToLower();
            if (response != "y") break;
        } while (true);

        Customer customer = new Customer(name, age, address, phone, customerPets);
        foreach (var pet in customerPets)
        {
            pet.Owner = customer;
        }
        return customer;
    }

    /// <summary>
    /// Register the customer in the main system repositories
    /// </summary>
    /// <param name="newCustomer">Customer to register</param>
    public static void RegisterCustomer(Customer newCustomer)
    {
        new RepositoryDict<Customer>().Add(newCustomer);
    }

    public static void UpdateCustomerMenu(RepositoryDict<Customer> customerDictRep)
    {
        Console.WriteLine("\n--- 📝 Update Customer ---");
        var customers = customerDictRep.GetAll().ToList();

        ViewCustomersById(customers);

        Console.Write("\n🎂 Enter the customer's ID: ");
        Guid id = Guid.Parse(Console.ReadLine()!);

        var customer = customerDictRep.GetById(id);
        if (!Validator.IsExist(customer, "❌ No customer found with that ID")) return;
        if (customer == null) return;

        // Update customer details
        string name = Validator.ValidateContentEmpty("\n👤 New name (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(name)) customer.Name = name;

        string ageInput = Validator.ValidateContentEmpty("\n🎂 New age (leave empty to keep current): ", allowEmpty: true);
        if (int.TryParse(ageInput, out int age)) customer.Age = age;

        string address = Validator.ValidateContentEmpty("\n🏠 New address (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(address)) customer.Address = address;

        string phone = Validator.ValidateContentEmpty("\n📞 New phone (leave empty to keep current): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(phone)) customer.Phone = phone;

        // Update pets
        Console.Write("\nDo you want to update a pet? (y/n): ");
        if (Console.ReadLine()!.Trim().ToLower() == "y")
        {
            PetService.ViewPets(customer.Pets);
            Console.Write("\nEnter the Pet ID you want to update: ");
            string input = Console.ReadLine()!.Trim();

            var pet = customer.Pets.FirstOrDefault(p => p.Id.ToString() == input);

            if (!Validator.IsExist(pet, "❌ No pet found with that ID")) return;
            if (pet == null) return;

            PetService.EditPet(pet);
            new RepositoryDict<Pet>().Update(pet);
            Console.WriteLine("\n✅ Pet updated successfully!");
        }

        UpdateCustomer(customer, customerDictRep);
        Console.WriteLine("\n✅ Customer updated successfully!");
    }


    public static void UpdateCustomer(Customer updateCustomer, RepositoryDict<Customer> customerDictRep)
    {
        customerDictRep.Update(updateCustomer);
    }


    public static void RemoveCustomer(RepositoryDict<Customer> customerDictRep)
    {
        // Solicitar al usuario que ingrese el ID del cliente
        Console.Write("\nEnter the customer ID to remove: ");
        string inputId = Console.ReadLine()!.Trim();

        // Verificar si el ID ingresado existe entre los clientes
        var customer = customerDictRep.GetAll().FirstOrDefault(c => c.Id.ToString() == inputId);

        if (!Validator.IsExist(customer, "❌ No customer found with that ID")) return;
        if (customer == null) return;

        // Mostrar el nombre del cliente a eliminar
        Console.WriteLine($"🗑️ Removing customer: {customer.Name} (ID: {customer.Id})");

        // Desvincular las mascotas del dueño eliminado
        foreach (var pet in customer.Pets)
        {
            Console.WriteLine($"🐾 Disassociating pet: {pet.Name} (ID: {pet.Id}) from {customer.Name}");
            pet.Owner = null; // Desvincular la mascota del dueño
        }

        // Eliminar el cliente del repositorio
        customerDictRep.Remove(customer.Id);

        // Confirmación de eliminación
        Console.WriteLine($"✅ Customer {customer.Name} and their pets have been successfully removed.");
    }



    public static void ShowAvailableCustomers(List<Customer> CustomerList)
    {
        Console.WriteLine("\n--- 👥 Available Customers ---");
        foreach (var c in CustomerList)
        {
            Console.WriteLine($"🆔 {c.Id} | 👤 {c.Name}");
        }
    }

    public static void ViewCustomers(List<Customer> CustomerList)
    {
        if (!Validator.IsExist(CustomerList, "⚠️  No customers found")) return;
        Console.WriteLine("\n--- 👥 Customer List ---");

        foreach (var customer in CustomerList)
        {
            Console.WriteLine($"\n🆔 ID: {customer.Id}");
            Console.WriteLine($"👤 Name: {customer.Name}");
            Console.WriteLine($"🎂 Age: {customer.Age}");
            Console.WriteLine($"🏠 Address: {customer.Address}");
            Console.WriteLine($"📞 Phone: {customer.Phone}");
            Console.WriteLine($"🐾 Pets Count: {customer.Pets.Count}");

            if (customer.Pets.Count > 0)
            {
                Console.WriteLine("\n   --- 🐶 Pets ---");
                foreach (var pet in customer.Pets)
                {
                    Console.WriteLine($"   🐾 Pet ID: {pet.Id}");
                    Console.WriteLine($"   📛 Name: {pet.Name}");
                    Console.WriteLine($"   🐕 Species: {pet.Species}");
                    Console.WriteLine($"   🐾 Breed: {pet.Breed}");
                    Console.WriteLine($"   🎂 Age: {pet.Age}");
                    Console.WriteLine();
                }
            }
        }
    }

    /// <summary>
    /// Displays information about a single customer and their pets.
    /// </summary>
    /// <param name="customer">Customer to display</param>
    public static void ViewSingleCustomer(Customer? customer)
    {
        if (!Validator.IsExist(customer, "⚠️  No customers found")) return;
        if (customer == null) return;

        Console.WriteLine($"\n🆔 ID: {customer.Id}");
        Console.WriteLine($"👤 Name: {customer.Name}");
        Console.WriteLine($"🎂 Age: {customer.Age}");
        Console.WriteLine($"🏠 Address: {customer.Address}");
        Console.WriteLine($"📞 Phone: {customer.Phone}");
        Console.WriteLine($"🐾 Pets Count: {customer.Pets.Count}");

        if (customer.Pets.Count > 0)
        {
            Console.WriteLine("\n   --- 🐶 Pets ---");
            foreach (var pet in customer.Pets)
            {
                Console.WriteLine($"   🐾 Pet ID: {pet.Id}");
                Console.WriteLine($"   📛 Name: {pet.Name}");
                Console.WriteLine($"   🐕 Species: {pet.Species}");
                Console.WriteLine($"   🐾 Breed: {pet.Breed}");
                Console.WriteLine($"   🎂 Age: {pet.Age}");
                Console.WriteLine();
            }
        }
    }

    public static void ViewCustomersById(List<Customer> CustomerList)
    {
        if (!Validator.IsExist(CustomerList, "⚠️  No customers found")) return;
        Console.WriteLine("\n--- 👥 Customer List ---");

        foreach (var customer in CustomerList)
        {
            Console.WriteLine($"\n🆔 ID: {customer.Id}");
            Console.WriteLine($"👤 Name: {customer.Name}");
        }
    }



    // FILTERS

    /// <summary>
    /// Searches for pets by the customer's ID.
    /// </summary>
    /// <param name="customerId">ID of the customer whose pets are to be searched</param>
    /// <returns>List of pets belonging to the specified customer</returns>
    public static List<Pet> SearchPetsByIdCustomer(Guid customerId)
    {
        var pets = new RepositoryDict<Pet>().GetAll();
        return pets.Where(p => p.Owner != null && p.Owner.Id == customerId).ToList();
    }

    /// <summary>
    /// Filter customers who have pets of a specific age and display the results
    /// </summary>
    /// <param name="CustomerList">List of customers</param>

    public static void FilterCustomersByPetAge(List<Customer> CustomerList)
    {
        if (!Validator.IsExist(CustomerList, "⚠️  No customers found")) return;

        Console.Write("\n🔍 Enter pet age to filter customers: ");
        int petAge;
        while (true)
        {
            try
            {
                petAge = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(petAge)) continue;
                break;
            }
            catch
            {
                Console.WriteLine("❌ Invalid input. Please enter a number");
                continue;
            }
        }

        var filteredCustomers = CustomerList
        .Select(c => new
        {
            Customer = c,
            Pets = c.Pets.Where(p => p.Age == petAge).ToList()
        })
        .Where(c => c.Pets.Any()).ToList();

        if (!Validator.IsExist(filteredCustomers, "⚠️  No customers found with pets of that age")) return;

        ShowPetsByAge(filteredCustomers, petAge);
    }

    /// <summary>
    /// Shows customers who have pets of a specific age.
    /// </summary>
    /// <param name="customersWithPets">Filtered customers</param>
    /// <param name="petAge">Age of the pet</param>
    public static void ShowPetsByAge(IEnumerable<dynamic> customersWithPets, int petAge)
    {

        Console.WriteLine("\n📋 --- Customer with Pets of Specified Age ---");
        foreach (var c in customersWithPets)
        {

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"\n👤 Customer: {c.Customer.Name}");
            Console.WriteLine($"📊 {petAge} year old pets: {c.Pets.Count}");

            foreach (var pet in c.Pets)
            {
                Console.WriteLine($"   🐾 {pet.Name} ({pet.Species}, {pet.Breed})");
            }
        }
        Console.WriteLine("\n----------------------------------------------------");
    }

    /// <summary>
    /// Allows the user to check the youngest or oldest customer.
    /// </summary>
    /// <param name="CustomerList">List of customers</param>
    public static void YoungestOrOlderCustomer(List<Customer> CustomerList)
    {
        if (!Validator.IsExist(CustomerList, "⚠️  No customers found")) return;

        int choose;
        while (true)
        {
            try
            {
                Console.WriteLine("Would you like to filter it?: \n1. Younger \n2. Older");
                choose = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(choose)) continue;
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
            Customer? selectedCustomer = null;
            switch (choose)
            {
                case 1:
                    selectedCustomer = YoungerCustomer(CustomerList);
                    break;
                case 2:
                    selectedCustomer = OlderCustomer(CustomerList);
                    break;
                default:
                    Console.WriteLine("⚠️  Invalid choice. Please try again");
                    continue;
            }
            ViewSingleCustomer(selectedCustomer);
            Console.WriteLine("----------------------------------------------------");
            break;
        }
    }

    /// <summary>
    /// Get the youngest customer on the list.
    /// </summary>
    /// <param name="customerList">List of customers</param>
    /// <returns>Youngest customer</returns>
    public static Customer? YoungerCustomer(List<Customer> customerList)
    {
        Console.WriteLine("\n--- 👤 Youngest Customer ---");
        Console.WriteLine("----------------------------------------------------");
        return customerList.OrderBy(c => c.Age).First();
    }

    /// <summary>
    /// Get the oldest customer from the list.
    /// </summary>
    /// <param name="customerList">Lista de clientes.</param>
    /// <returns>Oldest customer</returns>
    public static Customer? OlderCustomer(List<Customer> customerList)
    {
        Console.WriteLine("\n--- 👤 Oldest Customer ---");
        Console.WriteLine("----------------------------------------------------");
        return customerList.OrderByDescending(c => c.Age).First();
    }

    /// <summary>
    /// Shows customers who have at least one pet of unknown breed.
    /// </summary>
    /// <param name="customerList">List of customers</param>
    public static void CustomerUnknownPetBreed(List<Customer> customerList)
    {
        var selectedCustomers = customerList.Where(c => c.Pets.Any(p => p.Breed == "unknown")).ToList();

        if (!Validator.IsExist(selectedCustomers, "⚠️  No customers found with unknown pet breed")) return;

        Console.WriteLine("\n📋 --- Customers with Unknown Pet Breed ---");
        Console.WriteLine("----------------------------------------------------");
        ViewCustomers(selectedCustomers);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Displays customers sorted alphabetically by name in uppercase letters.
    /// </summary>
    /// <param name="CustomerList">List of customers</param>
    public static void CustomersInCapitalityAlphabetically(List<Customer> CustomerList)
    {
        if (!Validator.IsExist(CustomerList, "⚠️  No customers found")) return;

        var selectedCustomers = CustomerList.OrderBy(c => (c.Name ?? "").ToUpper()).ToList();

        Console.WriteLine("\n📋 --- Customers in Alphabetical Order (UPPERCASE) ---");
        Console.WriteLine("----------------------------------------------------");

        int index = 1;
        foreach (var customer in selectedCustomers)
        {
            Console.WriteLine($"👤 {index++}. Name: {customer.Name.ToUpper()}");
        }

        Console.WriteLine("----------------------------------------------------");
    }
}