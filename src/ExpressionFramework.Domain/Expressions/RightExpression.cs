namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the end of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get the last characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The last characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record RightExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetRightValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetRightValueFromString(string s)
    {
        var lengthResult = LengthExpression.Evaluate(s).TryCast<int>("LengthExpression did not return an integer");
        if (!lengthResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result<object?>.Success(s.Substring(s.Length - lengthResult.Value, lengthResult.Value))
            : Result<object?>.Invalid("Length must refer to a location within the string");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context, () => StringExpression.ValidateLength(context, LengthExpression));

    public RightExpression(int length) : this(new ConstantExpression(length)) { }
}

