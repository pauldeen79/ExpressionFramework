namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates an operator")]
[ContextType(typeof(object))]
[ContextDescription("The context to use on operator evaluation")]
[ContextRequired(false)]
[ParameterDescription(nameof(LeftExpression), "Left expression to use on operator")]
[ParameterRequired(nameof(LeftExpression), true)]
[ParameterDescription(nameof(RightExpression), "Right expression to use on operator")]
[ParameterRequired(nameof(RightExpression), true)]
[ParameterDescription(nameof(Operator), "Operator to evaluate")]
[ParameterRequired(nameof(Operator), true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "Result value of the operator evaluation", "This result will be returned when the evaluation succeeds")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case operator evaluation returns something else than Ok")]
public partial record OperatorExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<bool> EvaluateTyped(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);

    public Expression ToUntyped() => this;

    public OperatorExpression(object? leftExpression, object? rightExpression, Operator @operator) : this(new ConstantExpression(leftExpression), new ConstantExpression(rightExpression), @operator) { }
    public OperatorExpression(Func<object?, object?> leftExpression, Func<object?, object?> rightExpression, Operator @operator) : this(new DelegateExpression(leftExpression), new DelegateExpression(rightExpression), @operator) { }
}

public partial record OperatorExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
