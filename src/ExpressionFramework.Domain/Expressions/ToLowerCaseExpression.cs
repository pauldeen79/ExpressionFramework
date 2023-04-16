namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to lower case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the lower case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to lower case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToLowerCaseExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? Result<string>.Success(result.Value!.ToLower())
                : result);
}

public partial record ToLowerCaseExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
