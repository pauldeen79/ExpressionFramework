namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(LeftExpression))]
public partial record LeftExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

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
            return Result<string>.FromExistingResult(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result<string>.Success(s.Substring(0, lengthResult.Value))
            : Result<string>.Invalid("Length must refer to a location within the string");
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringEdgeDescriptor(
            typeof(LeftExpression),
            "Gets a number of characters of the start of a string value of the context",
            "String to get the first characters for",
            "The first characters of the expression",
            "This result will be returned when the context is of type string");
}
