namespace HealthClinic.repositories;

using HealthClinic.data;
using HealthClinic.models;
using HealthClinic.interfaces;

public class RepositoryDict<Entity> where Entity : class, IEntity
{
    private readonly Dictionary<Guid, Entity> _dataDict;

    public RepositoryDict()
    {
        if (typeof(Entity) == typeof(Customer))
            _dataDict = (Dictionary<Guid, Entity>)(object)Database.CustomersDict;
        else
            throw new InvalidOperationException($"No dictionary defined for {typeof(Entity).Name}");
    }

    public void Add(Entity entity)
    {
        _dataDict.TryAdd(entity.Id, entity);
    }

    public Entity? GetById(Guid id)
    {
        _dataDict.TryGetValue(id, out var entity);
        return entity;
    }

    public List<Entity> GetAll()
    {
        return _dataDict.Values.ToList();
    }

    public Dictionary<Guid, Entity> GetDictionary()
    {
        return _dataDict;
    }


    public void Remove(Guid id)
    {
        _dataDict.Remove(id);
    }

    public void Update(Entity entity)
    {
        if (_dataDict.ContainsKey(entity.Id))
            _dataDict[entity.Id] = entity;
    }
}
