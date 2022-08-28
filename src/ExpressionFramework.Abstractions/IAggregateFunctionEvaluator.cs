namespace ExpressionFramework.Abstractions;

public interface IAggregateFunctionEvaluator
{
    Result<IAggregateFunctionResultValue?> Evaluate(IAggregateFunction function,
                                                    bool isFirstItem,
                                                    object? value,
                                                    object? context,
                                                    IExpressionEvaluator evaluator,
                                                    IExpression expression);
}
