namespace ExpressionFramework.Domain.OperatorHandlers;

public abstract class OperatorHandlerBase : IOperatorHandler
{
    public Result<bool> Handle(Operator @operator, object? leftValue, object? rightValue)
    {
        if (@operator is not EqualsOperator)
        {
            return Result<bool>.NotSupported();
        }

        return Result<bool>.Success(Evaluate(leftValue, rightValue));
    }

    protected abstract bool Evaluate(object? leftValue, object? rightValue);
}
