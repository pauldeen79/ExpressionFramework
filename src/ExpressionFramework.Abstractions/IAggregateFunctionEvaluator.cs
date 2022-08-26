namespace ExpressionFramework.Abstractions;

public interface IAggregateFunctionEvaluator
{
    Result<IAggregateFunctionResultValue?> TryEvaluate(IAggregateFunction function,
                                                  bool isFirstItem,
                                                  object? value,
                                                  object? context,
                                                  IExpressionEvaluator evaluator,
                                                  IExpression expression);
}
