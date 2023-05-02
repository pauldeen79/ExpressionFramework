namespace ExpressionFramework.Parser.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Removes generics from a processed (fixed) typename. (<)
    /// </summary>
    /// <param name="typeName">Typename with or without generics</param>
    /// <returns>Typename without generics (<)</returns>
    public static string WithoutGenerics(this string instance)
    {
        var index = instance.IndexOf('<');
        return index == -1
            ? instance
            : instance.Substring(0, index);
    }

    public static string GetGenericArguments(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }
        //Bla<GenericArg1,...>

        var open = value.IndexOf("<");
        if (open == -1)
        {
            return string.Empty;
        }

        var comma = value.LastIndexOf(",");
        if (comma == -1)
        {
            comma = value.LastIndexOf(">");
        }

        if (comma == -1)
        {
            return string.Empty;
        }

        return value.Substring(open + 1, comma - open - 1);
    }
}
