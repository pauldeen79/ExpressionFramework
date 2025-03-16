namespace ExpressionFramework.Core.Functions.Operators;

public class EnumerableNotContainsOperatorFunction : ITypedFunction<IOperator>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IOperator> EvaluateTyped(FunctionCallContext context)
        => Result.Success<IOperator>(new EnumerableNotContainsOperator());
}
