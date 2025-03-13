namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("Value", typeof(bool))]
public class ConstantEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Value", () => context.GetArgumentBooleanValueResult(0, "Value"))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new ConstantEvaluatable(results.GetValue<bool>("Value"))));
}
