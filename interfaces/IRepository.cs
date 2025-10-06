namespace HealthClinic.repositories;

using HealthClinic.interfaces;
public interface IRepository<Entity> where Entity : class, IEntity
{
    void Add(Entity entity);
    List<Entity> GetAll();
    Entity? GetById(Guid id);
    void Remove(Guid id);
}
