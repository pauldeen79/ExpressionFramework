namespace ExpressionFramework.Abstractions;

public interface ICompositeFunctionEvaluator
{
    bool TryEvaluate(ICompositeFunction function,
                     bool isFirstItem,
                     object? previousValue,
                     object? context,
                     IExpressionEvaluator evaluator,
                     IExpression expression,
                     out object? result,
                     out bool shouldContinue);
}
