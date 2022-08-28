namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluatorHandler
{
    Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator);
}
