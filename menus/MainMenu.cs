namespace HealthClinic.menus;

using HealthClinic.repositories;
using HealthClinic.services;
using HealthClinic.models;
using HealthClinic.utils;

public class MainMenu
{
    // Crear repositorios una sola vez
    private static readonly Repository<Pet> _petRepo = new();
    private static readonly Repository<Veterinarian> _vetRepo = new();
    private static readonly Repository<Appointment> _appointmentRepo = new();
    private static readonly IRepository<Customer> _customerRepo = new RepositoryDict<Customer>();

    // Crear servicios una sola vez
    private static readonly AppointmentService _appointmentService = new(_petRepo, _vetRepo, _appointmentRepo);
    private static readonly PetService _petService = new(_petRepo, _customerRepo);

    // Crear menús pasando dependencias
    private static readonly AppointmentMenu _appointmentMenu = new(_appointmentService);
    private static readonly PetMenu _petMenu = new(_petService);

    public static void Menu()
    {
        Console.WriteLine("\n🐾 Welcome to HealthClinic System 🏥");
        Console.WriteLine("-----------------------------------");

        while (true)
        {
            try
            {
                ConsoleUI.ShowMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CustomerMenu.CustomerMainMenu();
                        continue;
                    case 2:
                        _petMenu.PetMainMenu();
                        continue;
                    case 3:
                        VeterinarianMenu.VeterinarianMainMenu();
                        continue;
                    case 4:
                        _appointmentMenu.AppointmentMainMenu();
                        continue;
                    case 5:
                        QuerysMenu.Querys();
                        continue;
                    case 6:
                        Console.WriteLine("\n👋 Thanks for using HealthClinic System. Goodbye! 🐶🐱");
                        break;
                    default:
                        Console.WriteLine("\n⚠️  Invalid choice. Please try again");
                        continue;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Invalid input. Please enter a valid number.");
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n⚠️  Unexpected error: {ex.Message}");
                continue;
            }
            break;
        }
    }
}
