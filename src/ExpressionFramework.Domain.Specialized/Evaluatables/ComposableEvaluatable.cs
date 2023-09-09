namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Evaluates one condition, which can be part of a composed condition")]
[ParameterDescription(nameof(LeftExpression), "Left expression")]
[ParameterRequired(nameof(LeftExpression), true)]
[ParameterDescription(nameof(Operator), "Operator to use")]
[ParameterRequired(nameof(Operator), true)]
[ParameterDescription(nameof(RightExpression), "Right expression")]
[ParameterRequired(nameof(RightExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true when the condition itself evaluates to true, otherwise false", "This result will be returned when evaluation of the expressions succeed")]
public partial record ComposableEvaluatable
{
    public ComposableEvaluatable(object? leftExpression, Operator @operator, object? rightExpression, bool startGroup = false, bool endGroup = false, Combination combination = Domains.Combination.And)
        : this(new ConstantExpression(leftExpression), @operator, new ConstantExpression(rightExpression), combination, startGroup, endGroup)
    {
    }
    
    public ComposableEvaluatable(Func<object?, object?> leftExpression, Func<Operator> @operator, Func<object?, object?> rightExpression, bool startGroup = false, bool endGroup = false, Combination combination = Domains.Combination.And)
        : this(new DelegateExpression(leftExpression), ArgumentGuard.IsNotNull(@operator, nameof(@operator)).Invoke(), new DelegateExpression(rightExpression), combination, startGroup, endGroup)
    {
    }

    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}

public partial record ComposableEvaluatableBase
{
    public override Result<bool> Evaluate(object? context) => throw new NotImplementedException();
}
