namespace ExpressionFramework.Domain.Expressions;

public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var firstValue = FirstExpression.Evaluate(context);
        if (!firstValue.IsSuccessful())
        {
            return firstValue;
        }

        var secondValue = SecondExpression.Evaluate(context);
        if (!secondValue.IsSuccessful())
        {
            return secondValue;
        }

        return Result<object?>.Success(EqualsOperator.IsValid(firstValue, secondValue));
    }
}
