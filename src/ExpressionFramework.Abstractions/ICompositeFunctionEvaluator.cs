namespace ExpressionFramework.Abstractions;

public interface ICompositeFunctionEvaluator
{
    ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                  bool isFirstItem,
                                                  object? value,
                                                  object? context,
                                                  IExpressionEvaluator evaluator,
                                                  IExpression expression);
}
