namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters from the specified position of a string value of the specified expression")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(IndexExpression), "Zero-based start position of the characters to return")]
[ParameterRequired(nameof(IndexExpression), true)]
[ParameterType(nameof(IndexExpression), typeof(int))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ParameterDescription(nameof(Expression), "String to get characters from")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "A set of characters of the context", "This result will be returned when the specified expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string, IndexExpression did not return an integer, LengthExpression did not return an integer, Index and length must refer to a location within the string")]
public partial record SubstringExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? GetSubstringFromString(result.Value!)
                : result);

    private Result<string> GetSubstringFromString(string s)
    {
        var indexResult = IndexExpression.EvaluateTyped(s);
        if (!indexResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(indexResult);
        }

        if (LengthExpression is null)
        {
            return Result.Success(s.Substring(indexResult.Value));
        }

        var lengthResult = LengthExpression!.EvaluateTyped(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(lengthResult);
        }

        return s.Length >= indexResult.Value + lengthResult.Value
            ? Result.Success(s.Substring(indexResult.Value, lengthResult.Value))
            : Result.Invalid<string>("Index and length must refer to a location within the string");
    }
}
