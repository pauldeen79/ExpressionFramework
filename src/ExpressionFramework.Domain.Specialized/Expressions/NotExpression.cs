namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the inverted value of the boolean value")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "Boolean to invert")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(bool))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "Inverted value of the boolean context value", "This result will be returned when the expression is a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type boolean")]
public partial record NotExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public Result<bool> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? Result<bool>.Success(!result.Value)
                : result);

    public NotExpression(bool expression) : this(new TypedConstantExpression<bool>(expression)) { }
    public NotExpression(Func<object?, bool> expression) : this(new TypedDelegateExpression<bool>(expression)) { }
}
