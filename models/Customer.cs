namespace HealthClinic.models;

public class Customer
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Ages { get; set; }
    public List<Pet> Pets { get; set; } = new List<Pet>();
}
