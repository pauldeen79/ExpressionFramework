namespace ExpressionFramework.Core.Functions.Operators;

public class IsSmallerOrEqualOperatorFunction : ITypedFunction<IOperator>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IOperator> EvaluateTyped(FunctionCallContext context)
        => Result.Success<IOperator>(new IsSmallerOrEqualOperator());
}
