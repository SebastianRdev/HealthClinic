namespace HealthClinic.utils;

/// <summary>
/// Utility class for common validations in the HealthClinic system.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Check if an integer is positive.
    /// </summary>
    /// <param name="number">Number to validate</param>
    /// <returns>True if positive, false if negative</returns>
    public static bool IsPositive(int number)
    {
        if (number < 0)
        {
            Console.WriteLine("⚠️  Please enter positive integers");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks whether a text string is not empty or consists only of spaces.
    /// </summary>
    /// <param name="text">Number to validate</param>
    /// <returns>True if not empty, false if empty or only spaces</returns>
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
