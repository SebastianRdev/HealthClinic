namespace HealthClinic.repositories;

/// Generic interface for repositories in the HealthClinic system. Allows CRUD operations on entities.
public interface IRepository<Entity> where Entity : class
{
    void Add(Entity entity);
    List<Entity> GetAll();
    Entity? GetById(Guid id);
    void Remove(Guid id);
    void Update(Entity entity);
}