namespace HealthClinic.models;

public class GeneralConsultation : VeterinaryService
{
    public override void Attend()
    {
        Console.WriteLine("Conducting a general consultation...");
    }
}
