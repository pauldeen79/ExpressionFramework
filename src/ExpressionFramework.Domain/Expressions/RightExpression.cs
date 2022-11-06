namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(RightExpression))]
public partial record RightExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? GetRightValueFromString(result.Value!)
                : result);

    private Result<string> GetRightValueFromString(string s)
    {
        var lengthResult = LengthExpression.EvaluateTyped<int>(s, "LengthExpression did not return an integer");
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

