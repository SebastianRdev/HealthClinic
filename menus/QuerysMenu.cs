using HealthClinic.utils;
using HealthClinic.services;

namespace HealthClinic.menus;

public class QuerysMenu
{
    private readonly PetService _petService;
    private readonly CustomerService _customerService;

    public QuerysMenu(PetService petService, CustomerService customerService)
    {
        _petService = petService;
        _customerService = customerService;
    }

    public void QuerysMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- 🔍 Queries Menu ---");
            Console.WriteLine("1. Sort Pets");
            Console.WriteLine("2. Group Pets by Species");
            Console.WriteLine("3. Filter Pets by Age");
            Console.WriteLine("4. Show Oldest Pet");
            Console.WriteLine("5. Sort Customers");
            Console.WriteLine("6. Group Customers by Address");
            Console.WriteLine("7. Filter Customers by Age");
            Console.WriteLine("8. Show Oldest Customer");
            Console.WriteLine("0. Exit");

            int option = Validator.ValidatePositiveInt("\n👉 Choose an option: ");
            Console.Clear();

            switch (option)
            {
                case 1: SortPetsUI(); break;
                case 2: GroupPetsBySpeciesUI(); break;
                case 3: FilterPetsByAgeUI(); break;
                case 4: ShowOldestPetUI(); break;
                case 5: SortCustomersUI(); break;
                case 6: GroupCustomersByAddressUI(); break;
                case 7: FilterCustomersByAgeUI(); break;
                case 8: ShowOldestCustomerUI(); break;
                case 0: return;
                default: Console.WriteLine("⚠️ Invalid option."); break;
            }
        }
    }

    // 🐾 PET QUERIES
    private void SortPetsUI()
    {
        try
        {
            int criteria = Validator.ValidatePositiveInt("Sort by:\n1. Name\n2. Age\n3. Species\n👉 ");
            int order = Validator.ValidatePositiveInt("\nOrder:\n1. Ascending\n2. Descending\n👉 ");

            var sortedPets = _petService.SortPets(criteria, order);
            Console.WriteLine("\n--- 🐾 Pets Sorted ---");
            foreach (var p in sortedPets)
                Console.WriteLine($"📛 {p.Name} | {p.Species} | Age: {p.Age}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void GroupPetsBySpeciesUI()
    {
        try
        {
            var grouped = _petService.GroupPetsBySpecies();
            Console.WriteLine("\n--- 🧬 Pets Grouped by Species ---");
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n🐕 Species: {group.Key}");
                foreach (var p in group)
                    Console.WriteLine($"   📛 {p.Name} | Age: {p.Age}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void FilterPetsByAgeUI()
    {
        try
        {
            int minAge = Validator.ValidatePositiveInt("Enter minimum age: ");
            var pets = _petService.FilterPetsByAge(minAge);
            Console.WriteLine($"\n--- 🐾 Pets with Age >= {minAge} ---");
            foreach (var p in pets)
                Console.WriteLine($"📛 {p.Name} | Age: {p.Age} | Species: {p.Species}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void ShowOldestPetUI()
    {
        try
        {
            var pet = _petService.GetOldestPet();
            Console.WriteLine("\n🐕 Oldest Pet:");
            Console.WriteLine($"📛 {pet.Name} | Age: {pet.Age} | Species: {pet.Species}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    // 👥 CUSTOMER QUERIES
    private void SortCustomersUI()
    {
        try
        {
            int criteria = Validator.ValidatePositiveInt("Sort by:\n1. Name\n2. Age\n3. Address\n👉 ");
            int order = Validator.ValidatePositiveInt("\nOrder:\n1. Ascending\n2. Descending\n👉 ");

            var sortedCustomers = _customerService.SortCustomers(criteria, order);
            Console.WriteLine("\n--- 👥 Customers Sorted ---");
            foreach (var c in sortedCustomers)
                Console.WriteLine($"📛 {c.Name} | {c.Age} years | {c.Address}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void GroupCustomersByAddressUI()
    {
        try
        {
            var grouped = _customerService.GroupCustomersByAddress();
            Console.WriteLine("\n--- 🏠 Customers Grouped by Address ---");
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n📍 Address: {group.Key}");
                foreach (var c in group)
                    Console.WriteLine($"   👤 {c.Name} | {c.Age} years");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void FilterCustomersByAgeUI()
    {
        try
        {
            int minAge = Validator.ValidatePositiveInt("Enter minimum age: ");
            var customers = _customerService.FilterCustomersByAge(minAge);
            Console.WriteLine($"\n--- 👥 Customers with Age >= {minAge} ---");
            foreach (var c in customers)
                Console.WriteLine($"📛 {c.Name} | {c.Age} years | {c.Address}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }

    private void ShowOldestCustomerUI()
    {
        try
        {
            var customer = _customerService.GetOldestCustomer();
            Console.WriteLine("\n👤 Oldest Customer:");
            Console.WriteLine($"📛 {customer.Name} | {customer.Age} years | {customer.Address}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
    }
}

