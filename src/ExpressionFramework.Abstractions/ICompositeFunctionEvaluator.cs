namespace ExpressionFramework.Abstractions;

public interface ICompositeFunctionEvaluator
{
    bool TryEvaluate(ICompositeFunction function, object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression, out object? result);
}
