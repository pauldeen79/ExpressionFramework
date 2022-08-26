namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluatorHandler
{
    Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator);
}
