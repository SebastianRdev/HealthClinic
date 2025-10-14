// namespace HealthClinic.menus;

// using HealthClinic.models;
// using HealthClinic.repositories;
// using HealthClinic.services;
// using HealthClinic.utils;
// public class QuerysMenu
// {
//     static RepositoryDict<Customer> customerDictRep = new RepositoryDict<Customer>();
//     static Repository<Pet> petRep = new Repository<Pet>();

//     /// <summary>
//     /// Displays the queries submenu and manages user navigation between different types of queries about customers and pets.
//     /// Allows you to filter, group, sort, and perform combined queries, as well as return to the main menu.
//     /// </summary>
//     public static void Querys()
//     {
//         while (true)
//         {
//             try
//             {
//                 ConsoleUI.ShowQueriesMenu();
//                 Console.Write("\n👉 Enter your choice: ");
//                 int choice = Convert.ToInt32(Console.ReadLine());
//                 switch (choice)
//                 {
//                     case 1:
//                         CustomerService.FilterCustomersByPetAge(customerDictRep.GetAll());
//                         continue;
//                     case 2:
//                         PetService.MainSort(petRep.GetAll());
//                         continue;
//                     case 3:
//                         PetService.GroupPetsBySpecies(petRep.GetAll());
//                         continue;
//                     case 4:
//                         PetService.CombinedConsultation(customerDictRep.GetAll());
//                         continue;
//                     case 5:
//                         CustomerService.YoungestOrOlderCustomer(customerDictRep.GetAll());
//                         continue;
//                     case 6:
//                         PetService.PetsOfEachSpecies(petRep.GetAll());
//                         continue;
//                     case 7:
//                         CustomerService.CustomerUnknownPetBreed(customerDictRep.GetAll());
//                         continue;
//                     case 8:
//                         CustomerService.CustomersInCapitalityAlphabetically(customerDictRep.GetAll());
//                         continue;
//                     case 9:
//                         break; // Back the main menu
//                     default:
//                         Console.WriteLine("\n⚠️  Invalid choice. Please try again");
//                         continue;
//                 }
//             }
//             catch
//             {
//                 Console.WriteLine("\n❌ Invalid input. Please enter a number, error: ");
//                 continue;
//             }
//             break;
//         }
//     }
// }
