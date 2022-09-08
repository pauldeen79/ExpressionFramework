namespace ExpressionFramework.Domain;

public interface IValueProvider
{
    Result<object?> GetValue(object? context, string fieldName);
}
