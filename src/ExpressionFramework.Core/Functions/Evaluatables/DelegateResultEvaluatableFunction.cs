namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Delegate", typeof(Func<bool>))]
public class DelegateResultEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Delegate", () => context.GetArgumentValueResult<Func<Result<bool>>>(0, "Delegate"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new DelegateResultEvaluatableBuilder().WithDelegate(results.GetValue<Func<Result<bool>>>("Delegate")).Build()));
}
