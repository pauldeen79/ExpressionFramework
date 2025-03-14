namespace ExpressionFramework.Core.Functions.Evaluatables;

[FunctionArgument("LeftExpression", typeof(object))]
[FunctionArgument("Operator", typeof(IOperator))]
[FunctionArgument("RightExpression", typeof(object))]
[FunctionArgument("StringComparison", typeof(StringComparison), false)]
public class OperatorEvaluatableFunction : ITypedFunction<IEvaluatable>
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => EvaluateTyped(context).Transform<object?>(x => x);

    public Result<IEvaluatable> EvaluateTyped(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("LeftExpression", () => context.GetArgumentValueResult(0, "LeftExpression"))
            .Add("Operator", () => context.GetArgumentValueResult<IOperator>(1, "Operator"))
            .Add("RightExpression", () => context.GetArgumentValueResult(2, "RightExpression"))
            .Add("StringComparison", () => context.GetArgumentValueResult(3, "StringComparison", StringComparison.InvariantCulture))
            .Build()
            .OnSuccess(results => Result.Success<IEvaluatable>(new OperatorEvaluatableBuilder()
                    .WithLeftValue(results.GetValue("LeftExpression"))
                    .WithOperator(results.GetValue<IOperator>("Operator").ToBuilder())
                    .WithRightValue(results.GetValue("RightExpression"))
                    .WithStringComparison(results.GetValue<StringComparison>("StringComparison"))
                    .Build())
            );
}
