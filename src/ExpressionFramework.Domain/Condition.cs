namespace ExpressionFramework.Domain;

public partial record Condition
{
    public Condition(Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(leftExpression, @operator, rightExpression, false, false, Combination.And)
    {
    }

    public Condition(Combination combination, Expression leftExpression, Operator @operator, Expression rightExpression)
        : this(leftExpression, @operator, rightExpression, false, false, combination)
    {
    }
}
