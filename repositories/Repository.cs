namespace HealthClinic.repositories;

using HealthClinic.interfaces;
using HealthClinic.data;
using HealthClinic.models;
public class Repository<Entity> : IRepository<Entity> where Entity : class, IEntity
{
    private readonly List<Entity> _dataList;

    public Repository()
    {
        if (typeof(Entity) == typeof(Customer))
            _dataList = (List<Entity>)(object)Database.Customers;
        else if (typeof(Entity) == typeof(Pet))
            _dataList = (List<Entity>)(object)Database.Pets;
        else if (typeof(Entity) == typeof(Veterinarian))
            _dataList = (List<Entity>)(object)Database.Veterinarians;
        else
            throw new InvalidOperationException($"There is no list for {typeof(Entity).Name} in Database");
    }

    public void Add(Entity entity) => _dataList.Add(entity);
    public List<Entity> GetAll() => _dataList;
    public Entity? GetById(Guid id) => _dataList.FirstOrDefault(e => e.Id == id);

    public void Remove(Guid id)
    {
        var entity = GetById(id);
        if (entity != null)
            _dataList.Remove(entity);
    }
}
