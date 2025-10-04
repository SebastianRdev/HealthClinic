namespace HealthClinic.models;

public class Animal
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Species { get; set; }

    public static void PlaySound()
    {
        Console.WriteLine($"\nThe dog makes a sound: Woof!");
        Console.WriteLine($"\nThe cat makes a sound: Meow!");
    }
}
