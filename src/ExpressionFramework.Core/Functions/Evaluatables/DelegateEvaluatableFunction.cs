namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Delegate", typeof(Func<object?, bool>))]
public class DelegateEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Delegate", () => context.GetArgumentValueResult<Func<object?, bool>>(0, "Delegate"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new DelegateEvaluatableBuilder().WithDelegate(results.GetValue<Func<object?, bool>>("Delegate")).Build()));
}
