namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Conditions", typeof(IEnumerable<ComposableEvaluatable>))]
public class ComposedEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Conditions", () => context.GetArgumentValueResult<IEnumerable<ComposableEvaluatable>>(0, "Conditions"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new ComposedEvaluatableBuilder()
                .AddConditions(results.GetValue<IEnumerable<ComposableEvaluatable>>("Conditions").Select(x => x.ToTypedBuilder()))
                .Build()));
}
