namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(MaxExpression))]
public partial record MaxExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, x => Result<object?>.Success(x.Max()), SelectorExpression);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(MaxExpression),
            "Gets the greatest value from the (enumerable) expression, optionally using a selector expression",
            "Smallest value",
            "This will be returned in case no error occurs",
            "Expression cannot be empty, Expression must be of type IEnumerable",
            "This status (or any other status not equal to Ok) will be returned in case the selector evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );

    public MaxExpression(IEnumerable expression) : this(new TypedConstantExpression<IEnumerable>(expression), null) { }
}
