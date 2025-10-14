namespace HealthClinic.menus;

using HealthClinic.services;
using HealthClinic.models;
using HealthClinic.utils;

public class CustomerMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public void CustomerMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowCustomerMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerCRUD();
                        continue;
                    case 2:
                        Console.WriteLine("\nBack to main menu 👥");
                        break;
                    default:
                        Console.WriteLine("\n⚠️ Invalid choice. Please try again");
                        continue;
                }
            }
            catch
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a number");
                continue;
            }
            break;
        }
    }

    public void CustomerCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowCustomerCRUD();
                Console.Write("\n👉 Enter your choice: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\n❌ Invalid input. Please enter a number");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RegisterCustomerUI();
                        continue;
                    case 2:
                        ViewCustomersUI();
                        continue;
                    case 3:
                        UpdateCustomerUI();
                        continue;
                    case 4:
                        RemoveCustomerUI();
                        continue;
                    case 5:
                        Console.WriteLine("\nBack to Customer main menu 👥");
                        break;
                    default:
                        Console.WriteLine("\n⚠️ Invalid choice. Please try again");
                        continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ {ex.Message}");
                continue;
            }
            break;
        }
    }

    private void RegisterCustomerUI()
    {
        Console.WriteLine("\n--- 📝 Register Customer 👤 ---");
        try
        {
            string name = Validator.ValidateContent("\n👤 Enter the customer's name: ");
            int age = Validator.ValidatePositiveInt("\n🎂 Enter the customer's age: ");
            string address = Validator.ValidateContent("\n🏠 Enter the customer's address: ");
            string phone = Validator.ValidateContent("\n📞 Enter the customer's phone: ");

            List<Pet> customerPets = new List<Pet>();

            Console.WriteLine("\n--- 🐕 Register Pet(s) ---");
            while (true)
            {
                string petName = Validator.ValidateContent("\n📛 Enter the pet's name: ");
                string petSpecies = Validator.ValidateContent("\n🐕 Enter the pet's species: ");
                string petBreed = Validator.ValidateContent("\n🐾 Enter the pet's breed (If you don't know, write: unknown): ");
                int petAge = Validator.ValidatePositiveInt("\n🎂 Enter the pet's age: ");

                Pet pet = new Pet(petName, petSpecies, petBreed, petAge);
                customerPets.Add(pet);
                pet.Register();

                Console.Write("\nDo you want to add another pet? (y/n): ");
                string? response = Console.ReadLine()?.Trim().ToLower();
                if (response != "y") break;
            }

            // Create and register customer
            Customer customer = new Customer(name, age, address, phone, customerPets);
            foreach (var pet in customerPets)
                pet.Owner = customer;

            _customerService.RegisterCustomer(name, age, address, phone, customerPets);
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Invalid input format. Please enter the data correctly.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"⚠️ {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex.Message}");
        }
    }


    private void ViewCustomersUI()
    {
        var customers = _customerService.ViewCustomers().ToList();

        if (customers.Count == 0)
        {
            Console.WriteLine("⚠️  No customers registered");
            return;
        }

        Console.WriteLine("\n--- 👥 Customer List ---");

        foreach (var customer in customers)
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
            Console.WriteLine("---------------------------------------");
        }
    }


    private void UpdateCustomerUI()
    {
        Console.WriteLine("\n--- ✏️  Update Customer ---");
        try
        {
            ViewCustomersUI();

            Console.Write("\nEnter Customer ID: ");
            var idInput = Console.ReadLine();

            if (!Guid.TryParse(idInput, out Guid customerId))
            {
                Console.WriteLine("⚠️  Invalid ID format");
                return;
            }

            // Obtener el cliente actual
            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("❌ No customer found with that ID");
                return;
            }

            Console.WriteLine($"\nCurrent data for {customer.Name}:");
            Console.WriteLine($"👤 Name: {customer.Name}");
            Console.WriteLine($"🎂 Age: {customer.Age}");
            Console.WriteLine($"🏠 Address: {customer.Address}");
            Console.WriteLine($"📞 Phone: {customer.Phone}");

            Console.WriteLine("\nUpdate fields (y/n):");

            string name = customer.Name;
            if (AskYesNo("Change name? (y/n): "))
                name = Validator.ValidateContent("👤 Enter new name: ");

            int age = customer.Age;
            if (AskYesNo("Change age? (y/n): "))
                age = Validator.ValidatePositiveInt("🎂 Enter new age: ");

            string address = customer.Address;
            if (AskYesNo("Change address? (y/n): "))
                address = Validator.ValidateContent("🏠 Enter new address: ");

            string phone = customer.Phone;
            if (AskYesNo("Change phone? (y/n): "))
                phone = Validator.ValidateContent("📞 Enter new phone: ");

            _customerService.UpdateCustomer(customerId, name, age, address, phone);
            Console.WriteLine("\n✅ Customer updated successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("⚠️  Invalid ID format. Please enter a valid value");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("❌ No customer found with that ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error updating customer: {ex.Message}");
        }
    }

    /// <summary>
    /// Helper para leer respuestas sí/no desde consola.
    /// </summary>
    private bool AskYesNo(string message)
    {
        Console.Write(message);
        var response = Console.ReadLine()?.Trim().ToLower();
        return response == "y" || response == "yes" || response == "s" || response == "si";
    }

    private void RemoveCustomerUI()
    {
        Console.WriteLine("\n--- 🗑 Remove Customer ---");
        try
        {
            ViewCustomersUI();

            Console.Write("\nEnter Customer ID: ");
            var idInput = Console.ReadLine();

            if (!Guid.TryParse(idInput, out Guid customerId))
            {
                Console.WriteLine("⚠️  Invalid ID format");
                return;
            }

            _customerService.RemoveCustomer(customerId);
            Console.WriteLine("\n✅ Customer removed successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("⚠️ Invalid ID format. Please enter a valid number.");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("❌ No customer found with that ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error removing customer: {ex.Message}");
        }
    }
}
