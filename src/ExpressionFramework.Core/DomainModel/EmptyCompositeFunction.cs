namespace ExpressionFramework.Core.DomainModel;

public partial record EmptyCompositeFunction
{
    public object? Combine(object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression)
        => Result.Error("No composite function selected");
}
