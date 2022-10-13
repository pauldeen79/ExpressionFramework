﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Skips a number of items on an enumerable context value")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to skip elements from")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(CountExpression), "Number of items to skip")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with skipped items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, CountExpression did not return an integer, Context must be of type IEnumerable")]
public partial record SkipExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var countResult = CountExpression.Evaluate(context);
        if (!countResult.IsSuccessful())
        {
            return countResult;
        }

        if (countResult.Value is not int count)
        {
            return Result<object?>.Invalid("CountExpression did not return an integer");
        }

        return context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Skip(count)
                .Select(x => Result<object?>.Success(x)))
            : Result<object?>.Invalid("Context must be of type IEnumerable");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => PerformAdditionalValidation(context));

    private IEnumerable<ValidationResult> PerformAdditionalValidation(object? context)
    {
        if (context is not IEnumerable e)
        {
            yield break;
        }

        var countResult = CountExpression.Evaluate(context);
        if (countResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"CountExpression returned an invalid result. Error message: {countResult.ErrorMessage}");
        }
        else if (countResult.Status == ResultStatus.Ok && countResult.Value is not int)
        {
            yield return new ValidationResult($"CountExpression did not return an integer");
        }
    }

    public SkipExpression(int count) : this(new ConstantExpression(count)) { }
}
