namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AnyExpression))]
public partial record AnyExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<object?>.Success(results.Any()),
            results => Result<object?>.Success(results.Any(x => x.Result.Value))
        );

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<bool> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<bool>.Success(results.Any()),
            results => Result<bool>.Success(results.Any(x => x.Result.Value))
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(AnyExpression),
            "Returns an indicator whether any item from the (enumerable) expression conform to the predicate, optionally using a predicate",
            "True when any item in the enumerable conform to the predicate, otherwise false",
            "This will be returned in case no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(bool)
        );

    public AnyExpression(IEnumerable expression, Func<object?, bool>? predicateExpression = null) : this(new TypedConstantExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression)) { }
    public AnyExpression(Func<object?, IEnumerable> expression, Func<object?, bool>? predicateExpression = null) : this(new TypedDelegateExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression)) { }
}

public partial record AnyExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
