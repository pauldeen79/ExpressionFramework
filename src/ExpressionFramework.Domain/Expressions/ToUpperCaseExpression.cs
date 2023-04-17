namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to upper case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the upper case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to upper case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToUpperCaseExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? Result<string>.Success(result.Value!.ToUpper())
                : result);

    public Expression ToUntyped() => this;

    public ToUpperCaseExpression(object? expression) : this(new ConstantExpression(expression)) { }
    public ToUpperCaseExpression(Func<object?, object?> expression) : this(new DelegateExpression(expression)) { }
}

public partial record ToUpperCaseExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
