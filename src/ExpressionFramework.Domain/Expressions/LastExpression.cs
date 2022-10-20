﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(LastExpression))]
public partial record LastExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetRequiredScalarValue
        (
            context,
            PredicateExpression,
            results => Result<object?>.Success(results.Last()),
            results => Result<object?>.Success(results.Last(x => x.Result.Value).Item)
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => EnumerableExpression.ValidateEmptyEnumerable(context));

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(LastExpression),
            "Gets the last value from the (enumerable) context value, optionally using a predicate to select an item",
            "Value of the last item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Context is not of type enumerable, Enumerable is empty, Predicate did not return a boolean value, None of the items conform to the supplied predicate",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            false
        );
}
