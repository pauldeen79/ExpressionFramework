﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(MaxExpression))]
public partial record MaxExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, x => Result<object?>.Success(x.Max()), SelectorExpression);

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(MaxExpression),
            "Gets the greatest value from the (enumerable) context value, optionally using a selector expression",
            "Smallest value",
            "This will be returned in case no error occurs",
            "Context cannot be empty, Context must be of type IEnumerable",
            "This status (or any other status not equal to Ok) will be returned in case the selector evaluation returns something else than Ok",
            false
        );
}

