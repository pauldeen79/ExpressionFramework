namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(LeftExpression))]
public partial record LeftExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? GetLeftValueFromString(result.Value!)
                : result);

    private Result<string> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.EvaluateTyped<int>(s, "LengthExpression did not return an integer");
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

public partial record LeftExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
