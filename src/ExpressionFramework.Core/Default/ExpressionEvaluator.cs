namespace ExpressionFramework.Core.Default;

public class ExpressionEvaluator : IExpressionEvaluator
{
    private readonly IEnumerable<IExpressionEvaluatorProvider> _expressionEvaluatorProviders;
    private readonly IEnumerable<IFunctionEvaluator> _functionEvaluators;

    public ExpressionEvaluator(IEnumerable<IExpressionEvaluatorProvider> expressionEvaluators, IEnumerable<IFunctionEvaluator> functionEvaluators)
    {
        _expressionEvaluatorProviders = expressionEvaluators;
        _functionEvaluators = functionEvaluators;
    }

    public Result<object?> Evaluate(object? item, object? context, IExpression expression)
    {
        var innerExpression = expression.InnerExpression;
        while (innerExpression != null)
        {
            var evalResult = Evaluate(item, context, innerExpression);
            if (!evalResult.IsSuccessful())
            {
                return evalResult;
            }
            item = evalResult.Value;
            innerExpression = innerExpression.InnerExpression;
        }

        object? expressionResult = null;
        var handled = false;
        foreach (var evaluatorProvider in _expressionEvaluatorProviders)
        {
            var result = evaluatorProvider.Evaluate(item, context, expression, this);
            if (result.IsSuccessful())
            {
                expressionResult = result.Value;
                handled = true;
                break;
            }
            else if (result.Status != ResultStatus.NotSupported)
            {
                // something went wrong... return the result
                return result;
            }
        }

        if (!handled)
        {
            return Result<object?>.NotSupported($"Unsupported expression: [{expression.GetType().Name}]");
        }

        if (expression.Function != null)
        {
            foreach (var evaluator in _functionEvaluators)
            {
                if (evaluator.TryEvaluate(expression.Function,
                                          expressionResult,
                                          item,
                                          expression,
                                          this,
                                          out var functionResult))
                {
                    return Result<object?>.Success(functionResult);
                }
            }
            return Result<object?>.NotSupported($"Unsupported function: [{expression.Function.GetType().Name}]");
        }

        return Result<object?>.Success(expressionResult);
    }
}
