namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public record FirstCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                         bool isFirstItem,
                                                         object? value,
                                                         object? context,
                                                         IExpressionEvaluator evaluator,
                                                         IExpression expression)
    {
        if (function is not FirstCompositeFunction)
        {
            return CompositeFunctionEvaluatorResultBuilder.NotSupported.Build();
        }

        var resultBuilder = CompositeFunctionEvaluatorResultBuilder.Supported;

        if (isFirstItem)
        {
            return resultBuilder.WithResult(value).Stop().Build();
        }

        return CompositeFunctionEvaluatorResultBuilder.Error("Not supposed to come here, as we said to stop!").Build();
    }
}
