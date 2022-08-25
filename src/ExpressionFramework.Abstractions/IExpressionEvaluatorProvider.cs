namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluatorProvider
{
    Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator);
}
