namespace ExpressionFramework.Domain;

public interface IExpressionEvaluator
{
    Result<object?> Evaluate(object? context, Expression expression);
}
