namespace HealthClinic.models;

public class Vaccination : VeterinarianService
{
    public override void Attend()
    {
        Console.WriteLine("Administering a vaccination...");
    }
}
