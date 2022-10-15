namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the start of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get the first characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The first characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record LeftExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.Evaluate(s).TryCast<int>("LengthExpression did not return an integer");
        if (!lengthResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result<object?>.Success(s.Substring(0, lengthResult.Value))
            : Result<object?>.Invalid("Length must refer to a location within the string");
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

