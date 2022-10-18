﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value on type")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to filter")]
[ContextRequired(true)]
[ParameterDescription(nameof(Type), "Type to filter on")]
[ParameterRequired(nameof(Type), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that are of the specified type", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable")]
public partial record OfTypeExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Where(x => x != null && Type.IsInstanceOfType(x))
                .Select(x => Result<object?>.Success(x)))
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);
}

