namespace ExpressionFramework.Domain.Evaluatables;

public partial record SingleEvaluatable
{
    public SingleEvaluatable(Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(leftExpression, @operator, rightExpression, false, false, Combination.And)
    {
    }

    public SingleEvaluatable(Combination combination, Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(leftExpression, @operator, rightExpression, false, false, combination)
    {
    }

    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}
