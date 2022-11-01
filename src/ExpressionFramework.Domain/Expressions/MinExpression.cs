namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(MinExpression))]
public partial record MinExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, x => Result<object?>.Success(x.Min()), SelectorExpression);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(MinExpression),
            "Gets the smallest value from the (enumerable) expression, optionally using a selector expression",
            "Smallest value",
            "This will be returned in case no error occurs",
            "Expression cannot be empty, Expression must be of type IEnumerable",
            "This status (or any other status not equal to Ok) will be returned in case the selector evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );
}

