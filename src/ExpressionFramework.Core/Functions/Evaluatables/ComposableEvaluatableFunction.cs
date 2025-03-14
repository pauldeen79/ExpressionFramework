namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("InnerEvaluatable", typeof(IEvaluatable))]
[FunctionArgument("Combination", typeof(Combination), false)]
[FunctionArgument("StartGroup", typeof(bool), false)]
[FunctionArgument("EndGroup", typeof(bool), false)]
public class ComposableEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("InnerEvaluatable", () => context.GetArgumentValueResult<IEvaluatable>(0, "InnerEvaluatable"))
            .Add("Combination", () => context.GetArgumentValueResult(1, "Combination", default(Combination?)))
            .Add("StartGroup", () => context.GetArgumentBooleanValueResult(2, "StartGroup", false))
            .Add("EndGroup", () => context.GetArgumentBooleanValueResult(3, "EndGroup", false))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new ComposableEvaluatableBuilder()
                .WithInnerEvaluatable(results.GetValue<IEvaluatable>("InnerEvaluatable").ToBuilder())
                .WithCombination(results.GetValue<Combination?>("Combination"))
                .WithStartGroup(results.GetValue<bool>("StartGroup"))
                .WithEndGroup(results.GetValue<bool>("EndGroup"))
                .Build()));
}
