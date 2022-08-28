namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluator
{
    Result<object?> Evaluate(object? context, IExpression expression);
}
