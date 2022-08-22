namespace ExpressionFramework.Abstractions;

public interface ICompositeFunctionEvaluator
{
    ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                  bool isFirstItem,
                                                  object? previousValue,
                                                  object? context,
                                                  IExpressionEvaluator evaluator,
                                                  IExpression expression);
}
