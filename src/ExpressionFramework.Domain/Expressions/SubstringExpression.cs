namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters from the specified position of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get a number of characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(IndexExpression), "Zero-based start position of the characters to return")]
[ParameterRequired(nameof(IndexExpression), true)]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ReturnValue(ResultStatus.Ok, "A set of characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record SubstringExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetLeftValueFromString(string s)
    {
        var indexResult = IndexExpression.Evaluate(s);
        if (!indexResult.IsSuccessful())
        {
            return indexResult;
        }
        if (indexResult.Value is not int index)
        {
            return Result<object?>.Invalid("IndexExpression did not return an integer");
        }

        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return lengthResult;
        }
        if (lengthResult.Value is not int length)
        {
            return Result<object?>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= index + length
                ? Result<object?>.Success(s.Substring(index, length))
                : Result<object?>.Invalid("Index and length must refer to a location within the string");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}

