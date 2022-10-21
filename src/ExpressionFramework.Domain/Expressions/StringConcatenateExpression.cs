namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Concatenates string")]
[UsesContext(false)]
[ParameterDescription(nameof(Expressions), "Strings to concatenate")]
[ParameterType(nameof(Expressions), typeof(string))]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(string), "The concatenated string", "This result will be returned when the expressions are all of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "At least one expression is required, Expression must be of type string")]
public partial record StringConcatenateExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
    {
        var values = Expressions.EvaluateTypedUntilFirstError<string>(context, "Expression must be of type string");
        if (!values.Any())
        {
            return Result<string>.Invalid("At least one expression is required");
        }

        if (!values.Last().IsSuccessful())
        {
            return Result<string>.FromExistingResult(values.Last());
        }

        return Result<string>.Success(string.Concat(values.Select(x => x.Value)));
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (!Expressions.Any())
        {
            yield return new ValidationResult("At least one expression is required");
            yield break;
        }

        foreach (var expression in Expressions)
        {
            var result = expression.Evaluate(context);
            if (result.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult(result.ErrorMessage);
                yield break;
            }

            if (result.Value is not string)
            {
                yield return new ValidationResult("Expression must be of type string");
                yield break;
            }
        }
    }
}

