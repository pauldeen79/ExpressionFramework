namespace ExpressionFramework.Domain;

public interface IExpressionEvaluatorHandler
{
    Result<object?> Handle(object? context, Expression expression, IExpressionEvaluator evaluator);
}
