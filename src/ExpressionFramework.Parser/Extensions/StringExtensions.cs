namespace ExpressionFramework.Parser.Extensions;

public static class StringExtensions
{
    public static Result<Type> GetGenericTypeResult(this string functionName)
    {
        var typeName = functionName.GetGenericArguments();
        if (string.IsNullOrEmpty(typeName))
        {
            return Result.Invalid<Type>("No type defined");
        }

        var type = Type.GetType(typeName);

        return type is not null
            ? Result.Success(type)
            : Result.Invalid<Type>($"Unknown type: {typeName}");
    }
}
