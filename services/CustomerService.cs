namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;

public class CustomerService
{
    public static void MainRegisterCustomer(List<Customer> CustomerList, Dictionary<Guid, Customer> CustomerDict)
    {
        Console.WriteLine("\n--- 📝 Register Customer ---");

        Customer newCustomer = RegisterCustomerMenu();

        RegisterCustomer(CustomerList, CustomerDict, newCustomer);

        Console.WriteLine("\n✅ Customer registered successfully");
    }


    public static Customer RegisterCustomerMenu()
    {
        string name;
        int age;
        string petName;
        string petSpecies;
        string petBreed;
        int petAge;

        while (true)
        {
            Console.Write("\n👤 Enter customer name: ");
            name = Console.ReadLine()!;
            if (!Validator.IsEmpty(name)) continue;
            break;
        }

        while (true)
        {
            try
            {
                Console.Write("\n🎂 Enter customer ages: ");
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

        // Pet details
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
            Console.Write("\n🐾 Enter the pet's breed: ");
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
        Customer customer = new Customer(name, age);
        customer.AddPet(pet);
        return customer;
    }

    public static void RegisterCustomer(List<Customer> CustomerList, Dictionary<Guid, Customer> CustomerDict, Customer newCustomer)
    {
        CustomerList.Add(newCustomer);
        CustomerDict[newCustomer.Id] = newCustomer;
    }

    public static void ViewCustomers(List<Customer> CustomerList)
    {
        Console.WriteLine("\n--- 👥 Customer List ---");
        if (CustomerList.Count == 0)
        {
            Console.WriteLine("⚠️  No customers found.");
            return;
        }

        foreach (var customer in CustomerList)
        {
            Console.WriteLine($"\n🆔 ID: {customer.Id}");
            Console.WriteLine($"👤 Name: {customer.Name}");
            Console.WriteLine($"🎂 Age: {customer.Age}");
            Console.WriteLine($"🐾 Pets Count: {customer.Pets.Count}");

            // Mostrar mascotas
            if (customer.Pets.Count > 0)
            {
                Console.WriteLine("   --- 🐶 Pets ---");
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
        ViewCustomers(foundCustomers);
    }


    // FILTERS
    public static void FilterByPetAge(List<Customer> CustomerList)
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

    public static void ShowPetsByAge(IEnumerable<dynamic> customersWithPets, int petAge)
    {
        foreach (var c in customersWithPets)
        {
            Console.WriteLine($"\n👤 Cliente: {c.Customer.Name}");
            Console.WriteLine($"📊 Mascotas de {petAge} años: {c.Pets.Count}");

            foreach (var pet in c.Pets)
            {
                Console.WriteLine($"   🐾 {pet.Name} ({pet.Species}, {pet.Breed})");
            }
        }
    }
}