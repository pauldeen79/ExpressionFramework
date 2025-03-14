namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Result", typeof(bool))]
public class ConstantResultEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Result", () => context.GetArgumentValueResult<Result<bool>>(0, "Result"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new ConstantResultEvaluatableBuilder().WithResult(results.GetValue<Result<bool>>("Result")).Build()));
}
