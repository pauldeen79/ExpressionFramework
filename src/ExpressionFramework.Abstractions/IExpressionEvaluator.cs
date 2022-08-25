namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluator
{
    Result<object?> Evaluate(object? item, object? context, IExpression expression);
}
