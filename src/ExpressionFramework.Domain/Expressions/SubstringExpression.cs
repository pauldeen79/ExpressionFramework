namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters from the specified position of a string value of the context")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(IndexExpression), "Zero-based start position of the characters to return")]
[ParameterRequired(nameof(IndexExpression), true)]
[ParameterType(nameof(IndexExpression), typeof(int))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ParameterDescription(nameof(Expression), "String to get characters for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "A set of characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, IndexExpression did not return an integer, LengthExpression did not return an integer, Index and length must refer to a location within the string")]
public partial record SubstringExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? GetSubstringFromString(result.Value!)
                : result);

    private Result<string> GetSubstringFromString(string s)
    {
        var indexResult = IndexExpression.Evaluate(s);
        if (!indexResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(indexResult);
        }

        if (indexResult.Value is not int index)
        {
            return Result<string>.Invalid("IndexExpression did not return an integer");
        }

        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        if (lengthResult.Value is not int length)
        {
            return Result<string>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= index + length
            ? Result<string>.Success(s.Substring(index, length))
            : Result<string>.Invalid("Index and length must refer to a location within the string");
    }

    public SubstringExpression(object? expression, object? indexExpression, object? lengthExpression) : this(new ConstantExpression(expression), new ConstantExpression(indexExpression), new ConstantExpression(lengthExpression)) { }
    public SubstringExpression(Func<object?, object?> expression, Func<object?, object?> indexExpression, Func<object?, object?> lengthExpression) : this(new DelegateExpression(expression), new DelegateExpression(indexExpression), new DelegateExpression(lengthExpression)) { }
}

public partial record SubstringExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
