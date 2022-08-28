namespace ExpressionFramework.Abstractions;

public interface IValueProvider
{
    Result<object?> GetValue(object? context, string fieldName);
}
