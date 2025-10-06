namespace HealthClinic.models;
public class GeneralConsultation : VeterinarianService
{
    public override void Attend()
    {
        Console.WriteLine("Conducting a general consultation...");
    }
}
