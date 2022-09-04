namespace ExpressionFramework.Domain;

public class ConditionEvaluatorProvider : IConditionEvaluatorProvider
{
    private readonly IEnumerable<IOperatorHandler> _handlers;

    public ConditionEvaluatorProvider(IEnumerable<IOperatorHandler> handlers)
        => _handlers = handlers;

    public IConditionEvaluator Get(IExpressionEvaluator expressionEvaluator)
        => new ConditionEvaluator(expressionEvaluator, _handlers);
}
