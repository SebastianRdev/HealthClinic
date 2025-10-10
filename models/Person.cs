namespace HealthClinic.models;

using HealthClinic.interfaces;

public abstract class Person : IEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
