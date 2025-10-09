namespace HealthClinic.utils;

using HealthClinic.models;

/// <summary>
/// Utility class for common validations in the HealthClinic system.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Solicita y valida que el contenido ingresado no esté vacío.
    /// </summary>
    /// <param name="prompt">Mensaje a mostrar</param>
    /// <returns>Texto validado</returns>
    public static string ValidateContent(string prompt)
    {
        string input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine()!;
            if (IsEmpty(input))
                return input;
        }
    }

    /// <summary>
    /// Solicita y valida que el número ingresado sea positivo.
    /// </summary>
    /// <param name="prompt">Mensaje a mostrar</param>
    /// <returns>Entero positivo validado</returns>
    public static int ValidatePositiveInt(string prompt)
    {
        int value;
        while (true)
        {
            try
            {
                Console.Write(prompt);
                value = Convert.ToInt32(Console.ReadLine());
                if (IsPositive(value))
                    return value;
            }
            catch
            {
                Console.WriteLine("❌ Invalid input. Please enter a number");
            }
        }
    }

    public static string ValidateContentEmpty(string prompt, bool allowEmpty = false)
    {
        string input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine()!;

            if (allowEmpty || !string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("⚠️  Empty spaces are not allowed");
        }
    }


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
            Console.WriteLine("⚠️  Empty spaces are not allowed");
            return false;
        }

        return true;
    }

    public static bool IsExist<T>(List<T>? list, string message)
    {
        if (list == null || list.Count == 0)
        {
            Console.WriteLine($"{message}");
            return false;
        }
        return true;
    }

    public static bool IsExist<T>(T? obj, string message) where T : class
    {
        if (obj == null)
        {
            Console.WriteLine($"{message}");
            return false;
        }
        return true;
    }

}
