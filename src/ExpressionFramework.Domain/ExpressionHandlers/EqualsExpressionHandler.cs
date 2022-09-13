namespace ExpressionFramework.Domain.ExpressionHandlers;

public class EqualsExpressionHandler : ExpressionHandlerBase<EqualsExpression>
{
    protected override async Task<Result<object?>> Handle(object? context, EqualsExpression typedExpression, IExpressionEvaluator evaluator)
    {
        var firstValue = await evaluator.Evaluate(context, typedExpression.FirstExpression);
        if (!firstValue.IsSuccessful())
        {
            return firstValue;
        }

        var secondValue = await evaluator.Evaluate(context, typedExpression.SecondExpression);
        if (!secondValue.IsSuccessful())
        {
            return secondValue;
        }

        return Result<object?>.Success(EqualsOperatorHandler.IsValid(firstValue, secondValue));
    }
}
