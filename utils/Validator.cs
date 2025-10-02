namespace HealthClinic.utils;

public static class Validator
{
    public static bool IsPositive(int number)
    {
        if (number < 0)
        {
            Console.WriteLine("⚠️  Please enter positive integers");
            return false;
        }

        return true;
    }

    public static bool IsEmpty(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("⚠️  Empty spaces are not allowed.");
            return false;
        }

        return true;
    }
}
