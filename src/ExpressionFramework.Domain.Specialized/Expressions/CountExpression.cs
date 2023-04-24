﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(CountExpression))]
public partial record CountExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<object?>.Success(results.Count()),
            results => Result<object?>.Success(results.Count(x => x.Result.Value))
        );

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<int> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<int>.Success(results.Count()),
            results => Result<int>.Success(results.Count(x => x.Result.Value))
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(CountExpression),
            "Gets the number of items from the (enumerable) expression, optionally using a predicate",
            "Number of items in the enumerable that conforms to the predicate",
            "This will be returned in case no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(int)
        );

    public CountExpression(IEnumerable expression, Func<object?, bool>? predicateExpression = null) : this(new TypedConstantExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression)) { }
    public CountExpression(Func<object?, IEnumerable> expression, Func<object?, bool>? predicateExpression = null) : this(new TypedDelegateExpression<IEnumerable>(expression), predicateExpression == null ? null : new TypedDelegateExpression<bool>(predicateExpression)) { }
}