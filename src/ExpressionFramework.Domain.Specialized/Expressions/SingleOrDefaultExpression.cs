namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SingleOrDefaultExpression))]
public partial record SingleOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<object?>.Success(results.Single()),
            results => Result<object?>.Success(results.Single(x => x.Result.Value).Item),
            context => EnumerableExpression.GetDefaultValue(DefaultExpression, context),
            items => items.Count(x => PredicateExpression is null || PredicateExpression.EvaluateTyped<bool>(x, "Predicate did not return a boolean value").Value) > 1
                ? Result<IEnumerable<object?>>.Invalid("Sequence contains more than one element")
                : Result<IEnumerable<object?>>.Success(items)
        );

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SingleOrDefaultExpression),
            "Gets a single value from the (enumerable) expression, optionally using a predicate to select an item",
            "Value of the single item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable contains a single element, and no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value, Sequence contains one than one element",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: true,
            resultValueType: typeof(object)
        );

    public SingleOrDefaultExpression(IEnumerable expression, Func<object?, bool>? predicateExpression = null, object? defaultExpression = null) : this(new TypedConstantExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression), defaultExpression == null ? null : new ConstantExpression(defaultExpression)) { }
    public SingleOrDefaultExpression(Func<object?, IEnumerable> expression, Func<object?, bool>? predicateExpression = null, Func<object?, object?>? defaultExpression = null) : this(new TypedDelegateExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression), defaultExpression == null ? null : new DelegateExpression(defaultExpression)) { }
}
