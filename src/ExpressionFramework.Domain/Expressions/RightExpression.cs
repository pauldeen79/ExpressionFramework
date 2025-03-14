namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(RightExpression))]
public partial record RightExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? GetRightValueFromString(result.Value)
                : result);

    private Result<string> GetRightValueFromString(string? s)
    {
        if (s is null)
        {
            return Result.Invalid<string>("Expression is not of type string");
        }

        var lengthResult = LengthExpression.EvaluateTyped(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result.Success(s.Substring(s.Length - lengthResult.Value, lengthResult.Value))
            : Result.Invalid<string>("Length must refer to a location within the string");
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringEdgeDescriptor(
            typeof(RightExpression),
            "Gets a number of characters of the end of a string value of the specified expression",
            "String to get the last characters for",
            "The last characters of the expression",
            "This result will be returned when the specified expression is of type string");
}
