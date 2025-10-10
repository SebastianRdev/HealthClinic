namespace HealthClinic.menus;

using HealthClinic.utils;
using HealthClinic.models;
using HealthClinic.services;
using HealthClinic.repositories;

public class AppointmentMenu
{
    static Repository<Pet> petRep = new Repository<Pet>();
    static Repository<Appointment> appointmentRep = new Repository<Appointment>();
    static Repository<Veterinarian> vetRep = new Repository<Veterinarian>();


    public static void AppointmentMainMenu()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsMainMenu();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AppointmentsCRUD();
                        continue;
                    case 2:
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

    public static void AppointmentsCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAppointmentsCRUD();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AppointmentService.RegisterAppointment(petRep, vetRep, appointmentRep);
                        continue;
                    case 2:
                        AppointmentService.ViewAppointments(appointmentRep.GetAll());
                        continue;
                    case 3:
                        AppointmentService.UpdateAppointment(appointmentRep.GetAll(), petRep, vetRep);
                        continue;
                    case 4:
                        AppointmentService.RemoveAppointment(appointmentRep.GetAll());
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
}
