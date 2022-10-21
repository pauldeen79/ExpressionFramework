namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the start of a string value of the context")]
[UsesContext(true)]
[ContextDescription("String to get the first characters for")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(string), "The first characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record LeftExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<string>.Invalid("Context must be of type string");

    private Result<string> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        if (lengthResult.Value is not int length)
        {
            return Result<string>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= length
            ? Result<string>.Success(s.Substring(0, length))
            : Result<string>.Invalid("Length must refer to a location within the string");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context, () => PerformAdditionalValidation(context));

    private IEnumerable<ValidationResult> PerformAdditionalValidation(object? context)
    {
        if (context is not string s)
        {
            yield break;
        }

        int? localLength = null;

        var lengthResult = LengthExpression.Evaluate(context);
        if (lengthResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"LengthExpression returned an invalid result. Error message: {lengthResult.ErrorMessage}");
        }
        else if (lengthResult.Status == ResultStatus.Ok)
        {
            if (lengthResult.Value is not int length)
            {
                yield return new ValidationResult($"LengthExpression did not return an integer");
            }
            else
            {
                localLength = length;
            }
        }

        if (localLength.HasValue && s.Length < localLength)
        {
            yield return new ValidationResult("Length must refer to a location within the string");
        }
    }

    public LeftExpression(int length) : this(new ConstantExpression(length)) { }
}

