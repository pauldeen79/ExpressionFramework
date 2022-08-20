namespace ExpressionFramework.Core.DomainModel;

public partial record EmptyCompositeFunction
{
    public object? Combine(object? previousValue, object? sourceItem, IExpressionEvaluator evaluator, IExpression expression)
        => Result.Error("No composite function selected");
}
