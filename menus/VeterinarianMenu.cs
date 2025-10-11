namespace HealthClinic.menus;

using HealthClinic.utils;
using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.repositories;

public class VeterinarianMenu
{
    static List<Veterinarian> vetList = new Repository<Veterinarian>().GetAll();

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

    public static void VeterinarianCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowVeterinarianCRUD();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        VeterinarianService.RegisterVeterinarian();
                        continue;
                    case 2:
                        VeterinarianService.ViewVeterinarians(vetList);
                        continue;
                    case 3:
                        VeterinarianService.UpdateVeterinarian(vetList);
                        continue;
                    case 4:
                        VeterinarianService.RemoveVeterinarian(vetList);
                        continue;
                    case 5:
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
}
