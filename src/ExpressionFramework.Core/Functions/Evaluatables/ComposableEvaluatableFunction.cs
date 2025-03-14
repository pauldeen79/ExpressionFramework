namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("LeftExpression", typeof(object))]
[FunctionArgument("Operator", typeof(IOperator))]
[FunctionArgument("RightExpression", typeof(object))]
[FunctionArgument("StringComparison", typeof(StringComparison), false)]
[FunctionArgument("Combination", typeof(Combination), false)]
[FunctionArgument("StartGroup", typeof(bool), false)]
[FunctionArgument("EndGroup", typeof(bool), false)]
public class ComposableEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("LeftExpression", () => context.GetArgumentValueResult(0, "LeftExpression"))
            .Add("Operator", () => context.GetArgumentValueResult<IOperator>(1, "Operator"))
            .Add("RightExpression", () => context.GetArgumentValueResult(2, "RightExpression"))
            .Add("StringComparison", () => context.GetArgumentValueResult(3, "StringComparison", StringComparison.InvariantCulture))
            .Add("Combination", () => context.GetArgumentValueResult(4, "Combination", default(Combination?)))
            .Add("StartGroup", () => context.GetArgumentBooleanValueResult(5, "StartGroup", false))
            .Add("EndGroup", () => context.GetArgumentBooleanValueResult(6, "EndGroup", false))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new ComposableEvaluatableBuilder()
                .WithInnerEvaluatable(
                    new OperatorEvaluatableBuilder()
                        .WithLeftValue(results.GetValue("LeftExpression"))
                        .WithOperator(results.GetValue<IOperator>("Operator").ToBuilder())
                        .WithRightValue(results.GetValue("RightExpression"))
                        .WithStringComparison(results.GetValue<StringComparison>("StringComparison")))
                .WithCombination(results.GetValue<Combination?>("Combination"))
                .WithStartGroup(results.GetValue<bool>("StartGroup"))
                .WithEndGroup(results.GetValue<bool>("EndGroup"))
                .Build()));
}
