namespace ExpressionFramework.Core.Default;

public class ExpressionEvaluator : IExpressionEvaluator
{
    private readonly IEnumerable<IExpressionEvaluatorHandler> _handlers;
    private readonly IEnumerable<IFunctionEvaluator> _evaluators;

    public ExpressionEvaluator(
        IEnumerable<IExpressionEvaluatorHandler> handlers,
        IEnumerable<IFunctionEvaluator> evaluators)
    {
        _handlers = handlers;
        _evaluators = evaluators;
    }

    public Result<object?> Evaluate(object? item, object? context, IExpression expression)
    {
        object? expressionResult = null;
        var handled = false;
        foreach (var handler in _handlers)
        {
            var result = handler.Handle(item, context, expression, this);
            if (result.IsSuccessful())
            {
                expressionResult = result.Value;
                handled = true;
                break;
            }
            else if (result.IsSupported())
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
            foreach (var evaluator in _evaluators)
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
