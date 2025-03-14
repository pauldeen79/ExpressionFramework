namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Transforms items from an enumerable expression using an expression")]
[ContextDescription("The enumerable value to transform elements for")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(SelectorExpression), "Expression to use on each item")]
[ParameterRequired(nameof(SelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with transformed items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Expression is not of type enumerable")]
public partial record SelectExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, e => EnumerableExpression.GetResultFromEnumerable(new TypedConstantExpression<IEnumerable>(e), context, e => e
            .Select(x => SelectorExpression.Evaluate(x))));

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, e => EnumerableExpression.GetTypedResultFromEnumerable(new TypedConstantExpression<IEnumerable>(e), context, e => e
            .Select(x => SelectorExpression.Evaluate(x))));
}
