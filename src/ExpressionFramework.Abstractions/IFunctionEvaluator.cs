namespace ExpressionFramework.Abstractions;

public interface IFunctionEvaluator
{
    bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpressionEvaluator evaluator, out object? result);
}
