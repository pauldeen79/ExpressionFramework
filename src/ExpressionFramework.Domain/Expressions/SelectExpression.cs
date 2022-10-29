namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Transforms items from an enumerable context value using an expression")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to transform elements for")]
[ContextRequired(true)]
[ParameterDescription(nameof(SelectorExpression), "Expression to use on each item")]
[ParameterRequired(nameof(SelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with transformed items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Expression is not of type enumerable")]
public partial record SelectExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, e => EnumerableExpression.GetResultFromEnumerable(e, e => e
            .Select(x => SelectorExpression.Evaluate(x))));
}

