namespace HealthClinic.models;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
