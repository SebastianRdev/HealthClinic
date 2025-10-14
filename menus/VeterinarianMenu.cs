namespace HealthClinic.menus;

using HealthClinic.utils;
using HealthClinic.services;
using HealthClinic.models.Enums;
public class VeterinarianMenu
{
    private readonly VeterinarianService _veterinarianService;

    public VeterinarianMenu(VeterinarianService veterinarianService)
    {
        _veterinarianService = veterinarianService;
    }

    public static void VeterinarianMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowVeterinarianMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        VeterinarianCRUD();
                        continue;
                    case 2:
                        AppointmentsVeterinarianMenu();
                        continue;
                    case 3:
                        Console.WriteLine("\nBack to main menu");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
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

    public void VeterinarianCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowVeterinarianCRUD();
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
                        RegisterVeterinarianUI();
                        continue;
                    case 2:
                        ViewVeterinariansUI();
                        continue;
                    case 3:
                        RemoveVeterinarianUI();
                        continue;
                    case 4:
                        UpdateVeterinarianUI();
                        continue;
                    case 5:
                        Console.WriteLine("\nBack to main menu");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
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

    public static void AppointmentsVeterinarianMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsVeterinarianMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        //
                        continue;
                    case 2:
                        //
                        continue;
                    case 3:
                        Console.WriteLine("\nBack to main menu");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
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

    private void RegisterVeterinarianUI()
    {
        try
        {
            Console.WriteLine("\n--- 📝 Register Veterinarian ---");

            Console.Write("\n👤 Name: ");
            string vetName = Console.ReadLine()!.Trim();

            Console.Write("\n🎂 Age: ");
            int vetAge = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("\n🏠 Address: ");
            string vetAddress = Console.ReadLine()!.Trim();

            Console.Write("\n📞 Phone: ");
            string vetPhone = Console.ReadLine()!.Trim();

            Console.Write("\n📧 Email: ");
            string vetEmail = Console.ReadLine()!.Trim();

            Console.WriteLine("\n🧼 --- Specialties ---");
            foreach (var specialty in Enum.GetValues(typeof(Specialties)))
                Console.WriteLine($"{(int)specialty}. {specialty}");

            Console.Write("\nEnter specialty (number): ");
            int specialtyInt = int.Parse(Console.ReadLine()!.Trim());
            var specialtyType = (Specialties)specialtyInt;

            // Register veterinarian
            var veterinarian = _veterinarianService.RegisterVeterinarian(vetName, vetAge, vetAddress, vetPhone, vetEmail);
            Console.WriteLine($"\n✅ Veterinarian registered successfully with ID: {veterinarian.Id}");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Invalid input format. Please enter the data correctly");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
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

    private void ViewVeterinariansUI()
    {
        
    }

    private void RemoveVeterinarianUI()
    {
        
    }

    private void UpdateVeterinarianUI()
    {
        
    }
}
