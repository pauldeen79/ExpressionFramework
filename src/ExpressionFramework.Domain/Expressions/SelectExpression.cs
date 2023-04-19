namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Transforms items from an enumerable context value using an expression")]
[ContextDescription("The enumerable value to transform elements for")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(SelectorExpression), "Expression to use on each item")]
[ParameterRequired(nameof(SelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with transformed items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Expression is not of type enumerable")]
public partial record SelectExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, e => EnumerableExpression.GetResultFromEnumerable(new ConstantExpression(e), context, e => e
            .Select(x => SelectorExpression.Evaluate(x))));

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, e => EnumerableExpression.GetTypedResultFromEnumerable(new ConstantExpression(e), context, e => e
            .Select(x => SelectorExpression.Evaluate(x))));

    public SelectExpression(object? expression, Expression selectorExpression) : this(new ConstantExpression(expression), selectorExpression) { }
    public SelectExpression(Func<object?, object?> expression, Expression selectorExpression) : this(new DelegateExpression(expression), selectorExpression) { }
}

public partial record SelectExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
