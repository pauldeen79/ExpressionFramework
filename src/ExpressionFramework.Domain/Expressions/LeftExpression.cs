namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(LeftExpression))]
public partial record LeftExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? GetLeftValueFromString(result.Value!)
                : result);

    private Result<string> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.EvaluateTyped(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result.Success(s.Substring(0, lengthResult.Value))
            : Result.Invalid<string>("Length must refer to a location within the string");
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringEdgeDescriptor(
            typeof(LeftExpression),
            "Gets a number of characters of the start of a string value of the specified expression",
            "String to get the first characters for",
            "The first characters of the expression",
            "This result will be returned when the specified expression is of type string");
}
