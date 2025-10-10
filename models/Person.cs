namespace HealthClinic.models;

public abstract class Person
{
    /// <summary>
    /// Unique customer identifier. Generated automatically when the instance is created.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string _name;
    public int _age;
    public string _address;
    public string _phone;
}
