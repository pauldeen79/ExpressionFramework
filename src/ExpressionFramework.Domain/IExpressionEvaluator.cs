namespace ExpressionFramework.Domain;

public interface IExpressionEvaluator
{
    Task<Result<object?>> Evaluate(object? context, Expression expression);
}
