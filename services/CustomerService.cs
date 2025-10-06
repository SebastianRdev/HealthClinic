namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;

public class CustomerService
{
    public static void MainRegisterCustomer(List<Customer> CustomerList, Dictionary<Guid, Customer> CustomerDict, List<Pet> pets)
    {
        Console.WriteLine("\n--- 📝 Register Customer ---");

        Customer newCustomer = RegisterCustomerMenu(pets);

        RegisterCustomer(CustomerList, CustomerDict, newCustomer);

        newCustomer.Register();
    }

    public static Customer RegisterCustomerMenu(List<Pet> globalPets)
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
            globalPets.Add(pet);
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

    public static Customer? YoungerCustomer(List<Customer> customerList)
    {
        Console.WriteLine("\n--- 👤 Youngest Customer ---");
        Console.WriteLine("----------------------------------------------------");
        return customerList.OrderBy(c => c.Age).First();
    }

    public static Customer? OlderCustomer(List<Customer> customerList)
    {
        Console.WriteLine("\n--- 👤 Oldest Customer ---");
        Console.WriteLine("----------------------------------------------------");
        return customerList.OrderByDescending(c => c.Age).First();
    }

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