namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluator
{
    object? Evaluate(object? item, object? context, IExpression expression);
}
