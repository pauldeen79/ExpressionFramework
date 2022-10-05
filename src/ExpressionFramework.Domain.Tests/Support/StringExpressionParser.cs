namespace ExpressionFramework.SpecFlow.Tests.Support;

public static class StringExpressionParser
{
    /// <summary>
    /// Replaces values specified from a table that need conversion, for example because the target type is object.
    /// </summary>
    /// <remarks>You can use expressions like [null] or [], [true], [false], [today], [1]</remarks>
    /// <param name="value">input value (automatically mapped table value)</param>
    /// <returns>Corrected value</returns>
    public static object? Parse(object? value)
    {
        if (value is not string s)
        {
            return value;
        }

        if (!(s.StartsWith("[") && s.EndsWith("]")))
        {
            return value;
        }

        return ParseInnerString(s.Substring(1, s.Length - 2).ToLowerInvariant(), value);
    }

    private static object? ParseInnerString(string innerString, object? defaultValue)
    {
        if (string.IsNullOrEmpty(innerString) || innerString == "null")
        {
            return null;
        }

        if (innerString == "today")
        {
            return DateTime.Today;
        }

        if (innerString == "space")
        {
            return " ";
        }

        if (bool.TryParse(innerString, out var b))
        {
            return b;
        }

        if (int.TryParse(innerString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
        {
            return i;
        }

        if (decimal.TryParse(innerString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var dec))
        {
            return dec;
        }

        if (DateTime.TryParseExact(innerString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            return dateTime;
        }

        return defaultValue;
    }
}
