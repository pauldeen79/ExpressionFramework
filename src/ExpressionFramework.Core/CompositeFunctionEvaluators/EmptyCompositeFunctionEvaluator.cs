namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

[ExcludeFromCodeCoverage]
internal class EmptyCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                         bool isFirstItem,
                                                         object? value,
                                                         object? context,
                                                         IExpressionEvaluator evaluator,
                                                         IExpression expression)
    {
        if (function is not EmptyCompositeFunction)
        {
            return CompositeFunctionEvaluatorResultBuilder.NotSupported.Build();
        }

        return CompositeFunctionEvaluatorResultBuilder.Error("No composite function was selected").Build();
    }
}
