namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the inverted value of the boolean value")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "Boolean to invert")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(bool))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "Inverted value of the boolean context value", "This result will be returned when the expression is a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type boolean")]
public partial record NotExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<bool> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<bool>(context, "Expression must be of type boolean").Transform(result =>
            result.IsSuccessful()
                ? Result<bool>.Success(!result.Value!)
                : result);

    public NotExpression(object? expression) : this(new ConstantExpression(expression)) { }
    public NotExpression(Func<object?, object?> expression) : this(new DelegateExpression(expression)) { }
}

public partial record NotExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
