namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(RightExpression))]
public partial record RightExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? GetRightValueFromString(result.Value)
                : result);

    private Result<string> GetRightValueFromString(string? s)
    {
        if (s is null)
        {
            return Result<string>.Invalid("Expression is not of type string");
        }

        var lengthResult = LengthExpression.EvaluateTyped(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result<string>.Success(s.Substring(s.Length - lengthResult.Value, lengthResult.Value))
            : Result<string>.Invalid("Length must refer to a location within the string");
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringEdgeDescriptor(
            typeof(RightExpression),
            "Gets a number of characters of the end of a string value of the context",
            "String to get the last characters for",
            "The last characters of the expression",
            "This result will be returned when the context is of type string");
}
