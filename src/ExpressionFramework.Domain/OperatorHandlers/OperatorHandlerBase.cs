namespace ExpressionFramework.Domain.OperatorHandlers;

public abstract class OperatorHandlerBase<T> : IOperatorHandler
    where T : Operator
{
    public Result<bool> Handle(Operator @operator, object? leftValue, object? rightValue)
    {
        if (@operator is not T)
        {
            return Result<bool>.NotSupported();
        }

        return Result<bool>.Success(Evaluate(leftValue, rightValue));
    }

    protected abstract bool Evaluate(object? leftValue, object? rightValue);
}
