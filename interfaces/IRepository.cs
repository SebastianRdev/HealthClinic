namespace HealthClinic.repositories;

using HealthClinic.interfaces;

/// <summary>
/// Generic interface for repositories in the HealthClinic system. Allows CRUD operations on entities.
/// </summary>
/// <typeparam name="Entity">Type of entity managed by the repository</typeparam>
public interface IRepository<Entity> where Entity : class, IEntity
{
    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">Entity to add</param>
    void Add(Entity entity);

    /// <summary>
    /// Get all entities from the repository.
    /// </summary>
    /// <returns>List of entities</returns>
    List<Entity> GetAll();

    /// <summary>
    /// Obtains an entity by its unique identifier.
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>Entity found or null if it does not exist</returns>
    Entity? GetById(Guid id);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to be deleted</param>
    void Remove(Guid id);
}
