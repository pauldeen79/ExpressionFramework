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
    public ComposableEvaluatable(Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(false, false, Combination.And, leftExpression, @operator, rightExpression)
    {
    }

    public ComposableEvaluatable(Combination combination, Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(false, false, combination, leftExpression, @operator, rightExpression)
    {
    }

    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}

public partial record ComposableEvaluatableBase
{
    public override Result<bool> Evaluate(object? context) => throw new NotImplementedException();
}
