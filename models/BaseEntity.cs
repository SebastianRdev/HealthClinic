namespace HealthClinic.models;

/// <summary>
/// Abstract base class for system entities. Provides a unique identifier (Id) for each entity.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity. It is automatically generated when the instance is created.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
