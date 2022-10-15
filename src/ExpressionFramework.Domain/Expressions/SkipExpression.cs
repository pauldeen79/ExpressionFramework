﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Skips a number of items on an enumerable context value")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to skip elements from")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(CountExpression), "Number of items to skip")]
[ParameterRequired(nameof(CountExpression), true)]
[ParameterType(nameof(CountExpression), typeof(int))]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with skipped items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, CountExpression did not return an integer, Context must be of type IEnumerable")]
public partial record SkipExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var countResult = CountExpression.Evaluate(context).TryCast<int>("CountExpression did not return an integer");
        if (!countResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(countResult);
        }

        return context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Skip(countResult.Value)
                .Select(x => Result<object?>.Success(x)))
            : Result<object?>.Invalid("Context must be of type IEnumerable");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => IntExpression.ValidateParameter(context, CountExpression, nameof(CountExpression)));

    public SkipExpression(int count) : this(new ConstantExpression(count)) { }
}
