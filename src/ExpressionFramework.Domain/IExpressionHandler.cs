namespace ExpressionFramework.Domain;

public interface IExpressionHandler
{
    Task<Result<object?>> Handle(object? context, Expression expression, IExpressionEvaluator evaluator);
}
