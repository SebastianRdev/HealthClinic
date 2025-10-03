namespace HealthClinic.services;

using HealthClinic.models;
using HealthClinic.utils;

public class PetService
{
    public static void ViewPets(List<Pet> PetList)
    {
        if (PetList.Count == 0)
        {
            Console.WriteLine("\n⚠️  No pets found.");
            return;
        }

        Console.WriteLine("\n--- 🐾 Pets List ---");
        foreach (var pet in PetList)
        {
            Console.WriteLine($"\nID: {pet.Id}");
            Console.WriteLine($"Name: {pet.Name}");
            Console.WriteLine($"Species: {pet.Species}");
            Console.WriteLine($"Breed: {pet.Breed}");
            Console.WriteLine($"Age: {pet.Age}");
            Console.WriteLine($"Owner: {pet.Owner.Name} (ID: {pet.Owner.Id})");
        }
    }

    // QUERIES
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
                Console.WriteLine("In what order?: \n1. Ascending \n2. Descending");
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
                Console.WriteLine("⚠️ Invalid choice. Please try again");
                break;
        }
    }

    public static void SortByName(List<Pet> PetList, int order)
    {
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Name).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Name).ToList();
        ViewPets(sortedPets);
    }

    public static void SortByAge(List<Pet> PetList, int order)
    {
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Age).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Age).ToList();
        ViewPets(sortedPets);
    }

    public static void SortBySpecies(List<Pet> PetList, int order)
    {
        List<Pet> sortedPets;
        if (order == 1)
            sortedPets = PetList.OrderBy(c => c.Species).ToList();
        else
            sortedPets = PetList.OrderByDescending(c => c.Species).ToList();
        ViewPets(sortedPets);
    }

    public static void GroupPetsBySpecies(List<Pet> PetList)
    {
        if (PetList.Count == 0)
        {
            Console.WriteLine("\n⚠️ No pets found.");
            return;
        }

        var groupedPets = PetList
            .GroupBy(p => p.Species) // agrupamos por especie
            .OrderBy(g => g.Key);   // opcional: orden alfabético

        foreach (var group in groupedPets)
        {
            Console.WriteLine($"\n🐾 Species: {group.Key} ({group.Count()} pets)");

            foreach (var pet in group)
            {
                Console.WriteLine($"   📛 {pet.Name} - Breed: {pet.Breed}, Age: {pet.Age}");
                Console.WriteLine($"      👤 Owner: {pet.Owner?.Name ?? "No owner"}");
            }
        }
    }

    public static void CombinedConsultation(List<Customer> CustomerList)
    {
        var result = CustomerList
            .Where(c => c.Pets.Any(p => p.Species.Equals("dog", StringComparison.OrdinalIgnoreCase) && p.Age == 3))
            .Select(c => new { c.Name, c.Phone })
            .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("\n⚠️ No clients found with a 3-year-old dog.");
            return;
        }

        Console.WriteLine("\n--- 🐶 Clients with a 3-year-old dog ---");
        foreach (var entry in result)
        {
            Console.WriteLine($"👤 Name: {entry.Name}, 📞 Phone: {entry.Phone}");
        }
    }
}
