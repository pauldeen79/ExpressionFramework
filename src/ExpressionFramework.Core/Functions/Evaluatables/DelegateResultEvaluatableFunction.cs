namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Delegate", typeof(Func<object?, Result<bool>>))]
public class DelegateResultEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Delegate", () => context.GetArgumentValueResult<Func<object?, Result<bool>>>(0, "Delegate"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new DelegateResultEvaluatableBuilder().WithDelegate(results.GetValue<Func<object?, Result<bool>>>("Delegate")).Build()));
}
