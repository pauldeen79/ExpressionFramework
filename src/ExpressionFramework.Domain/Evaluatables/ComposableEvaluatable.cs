namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Evaluates one condition, which can be part of a composed condition")]
[EvaluatableParameterDescription(nameof(LeftExpression), "Left expression")]
[EvaluatableParameterRequired(nameof(LeftExpression), true)]
[EvaluatableParameterDescription(nameof(Operator), "Operator to use")]
[EvaluatableParameterRequired(nameof(Operator), true)]
[EvaluatableParameterDescription(nameof(RightExpression), "Right expression")]
[EvaluatableParameterRequired(nameof(RightExpression), true)]
[EvaluatableReturnValue(ResultStatus.Ok, "true when the condition itself evaluates to true, otherwise false", "This result will be returned when evaluation of the expressions succeed")]
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

