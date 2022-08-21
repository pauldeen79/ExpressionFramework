namespace ExpressionFramework.Core.FunctionEvaluators;

//TODO: Check if we can remove this class
public class ConditionalExpressionExpressionResultFunctionEvaluator : IFunctionEvaluator
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public ConditionalExpressionExpressionResultFunctionEvaluator(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        result = null;
        if (!(function is ConditionalExpressionExpressionResultFunction))
        {
            return false;
        }

        var conditionalExpression = expression as IConditionalExpression;
        if (conditionalExpression != null)
        {
            result = _conditionEvaluatorProvider.Get(evaluator).Evaluate(sourceItem, conditionalExpression.Conditions);
        }

        return true;
    }
}
