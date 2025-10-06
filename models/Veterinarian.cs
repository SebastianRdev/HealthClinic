namespace HealthClinic.models;

using HealthClinic.interfaces;

public class Veterinarian : IEntity
{
    public Guid Id { get; private set; }
    public string? Name { get; set; }
    public string? Specialization { get; set; }

    public Veterinarian(string name, string specialization)
    {
        Id = Guid.NewGuid();
        Name = name;
        Specialization = specialization;
    }
}