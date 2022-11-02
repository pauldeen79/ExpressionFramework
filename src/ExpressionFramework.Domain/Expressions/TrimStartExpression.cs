namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Trims the start characters of the expression")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the trimmed value for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ParameterDescription(nameof(TrimCharsExpression), "Optional trim characters to use. When empty, space will be used")]
[ParameterRequired(nameof(TrimCharsExpression), false)]
[ReturnValue(ResultStatus.Ok, typeof(string), "The trim start value of the expression", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record TrimStartExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? TrimStart(context, result.Value!)
                : result);


    public TrimStartExpression(Expression expression) : this(expression, null) { }

    private Result<string> TrimStart(object? context, string s)
    {
        if (TrimCharsExpression == null)
        {
            return Result<string>.Success(s.TrimStart());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped<char[]>(context, "TrimCharsExpression must be of type char[]");
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
        }

        return Result<string>.Success(s.TrimStart(trimCharsResult.Value!));
    }
}

