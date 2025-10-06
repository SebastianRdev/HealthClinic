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
        string name;
        int age;
        string address;
        string phone;

        while (true)
        {
            Console.Write("\n👤 Enter the customer's name: ");
            name = Console.ReadLine()!;
            if (!Validator.IsEmpty(name)) continue;
            break;
        }

        while (true)
        {
            try
            {
                Console.Write("\n🎂 Enter the customer's age: ");
                age = Convert.ToInt32(Console.ReadLine());
                if (!Validator.IsPositive(age)) continue;
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
            Console.Write("\n🏠 Enter the customer's address: ");
            address = Console.ReadLine()!;
            if (!Validator.IsEmpty(address)) continue;
            break;
        }

        while (true)
        {
            Console.Write("\n📞 Enter the customer's phone: ");
            phone = Console.ReadLine()!;
            if (!Validator.IsEmpty(phone)) continue;
            break;
        }

        List<Pet> customerPets = new List<Pet>();

        do
        {
            string petName, petSpecies, petBreed;
            int petAge;

            Console.WriteLine("\n--- 📝 Register Pet 🐕 ---");
            while (true)
            {
                Console.Write("\n📛 Enter the pet's name: ");
                petName = Console.ReadLine()!;
                if (!Validator.IsEmpty(petName)) continue;
                break;
            }

            while (true)
            {
                Console.Write("\n🐕 Enter the pet's species: ");
                petSpecies = Console.ReadLine()!;
                if (!Validator.IsEmpty(petSpecies)) continue;
                break;
            }

            while (true)
            {
                Console.Write("\n🐾 Enter the pet's breed(If you don't know, write: unknown): ");
                petBreed = Console.ReadLine()!;
                if (!Validator.IsEmpty(petBreed)) continue;
                break;
            }

            while (true)
            {
                try
                {
                    Console.Write("\n🎂 Enter the pet's age: ");
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
        new Repository<Customer>().Add(newCustomer);
        new RepositoryDict<Customer>().Add(newCustomer);
    }

    /// <summary>
    /// Display the complete list of customers and their pets in the console.
    /// </summary>
    /// <param name="CustomerList">List of customers to display</param>
    public static void ViewCustomers(List<Customer> CustomerList)
    {
        Console.WriteLine("\n--- 👥 Customer List ---");
        if (CustomerList.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found");
            return;
        }

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
        if (customer == null)
        {
            Console.WriteLine("⚠️  No customers found.");
            return;
        }

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


    /// <summary>
    /// Search for customers by name and display the results found.
    /// </summary>
    /// <param name="CustomerList">List of customers</param>
    /// <param name="name">Name to display</param>
    public static void SearchCustomerByName(List<Customer> CustomerList, string name)
    {
        Console.Write("\n🔍 Enter customer name to search: ");
        string searchName = Console.ReadLine()!;
        var foundCustomers = CustomerList.Where(c => c.Name!.Equals(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
        if (foundCustomers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found with that name.");
            return;
        }
        Console.WriteLine($"\n📋 --- Customers Found with Name: {searchName} ---");
        Console.WriteLine("----------------------------------------------------");
        ViewCustomers(foundCustomers);
        Console.WriteLine("----------------------------------------------------");
    }


    // FILTERS
    /// <summary>
    /// Filter customers who have pets of a specific age and display the results
    /// </summary>
    /// <param name="CustomerList">List of customers</param>
    public static void FilterCustomersByPetAge(List<Customer> CustomerList)
    {
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

        if (filteredCustomers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found with pets of that age.");
            return;
        }
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
        if (CustomerList.Count == 0)
        {
            Console.WriteLine("\n⚠️  No customers found.");
            return;
        }

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
        if (selectedCustomers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found with unknown pet breed");
            return;
        }
        Console.WriteLine("\n📋 --- Customers with Unknown Pet Breed ---");
        Console.WriteLine("----------------------------------------------------");
        ViewCustomers(selectedCustomers);
        Console.WriteLine("----------------------------------------------------");
    }

    /// <summary>
    /// Displays customers sorted alphabetically by name in uppercase letters.
    /// </summary>
    /// <param name="customerList">List of customers</param>
    public static void CustomersInCapitalityAlphabetically(List<Customer> customerList)
    {
        var selectedCustomers = customerList.OrderBy(c => c.Name.ToUpper()).ToList();

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