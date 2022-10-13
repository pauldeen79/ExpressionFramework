namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the start of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get the first characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ReturnValue(ResultStatus.Ok, "The first characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record LeftExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return lengthResult;
        }

        if (lengthResult.Value is not int length)
        {
            return Result<object?>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= length
            ? Result<object?>.Success(s.Substring(0, length))
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

        int? localIndex = null;
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

        if (localIndex.HasValue && localLength.HasValue && s.Length < localIndex + localLength)
        {
            yield return new ValidationResult("Index and length must refer to a location within the string");
        }
    }

    public LeftExpression(int length) : this(new ConstantExpression(length)) { }
}

