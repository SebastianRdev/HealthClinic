namespace HealthClinic.menus;

using HealthClinic.services;
using HealthClinic.utils;
using HealthClinic.repositories;
using HealthClinic.models;
public class PetMenu
{
    static List<Pet> petList = new Repository<Pet>().GetAll();
    public static void PetCRUD()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowPetCRUD();
                Console.Write("\n👉 Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PetService.RegisterPet();
                        continue;
                    case 2:
                        PetService.ViewPets(petList);
                        continue;
                    case 3:
                        PetService.UpdatedPet(petList);
                        continue;
                    case 4:
                        PetService.RemovePet(petList);
                        continue;
                    case 5:
                        Console.WriteLine("\n Back the main menu 🐶🐱");
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
