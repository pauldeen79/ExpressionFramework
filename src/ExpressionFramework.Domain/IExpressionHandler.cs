namespace ExpressionFramework.Domain;

public interface IExpressionHandler
{
    Result<object?> Handle(object? context, Expression expression, IExpressionEvaluator evaluator);
}
