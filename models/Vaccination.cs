namespace HealthClinic.models;

public class Vaccination : VeterinaryService
{
    public override void Attend()
    {
        Console.WriteLine("Administering a vaccination...");
    }
}
