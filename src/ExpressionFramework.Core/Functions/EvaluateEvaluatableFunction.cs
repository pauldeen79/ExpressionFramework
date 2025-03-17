namespace ExpressionFramework.Core.Functions;

[FunctionArgument("Evaluatable", typeof(IEvaluatable))]
public class EvaluateEvaluatableFunction : ITypedFunction<bool>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<bool> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
        .Add("Evaluatable", () => context.GetArgumentValueResult<IEvaluatable>(0, "Evaluatable"))
        .Build()
        .OnSuccess(results => results.GetValue<IEvaluatable>("Evaluatable").Evaluate(context.Context));
}
