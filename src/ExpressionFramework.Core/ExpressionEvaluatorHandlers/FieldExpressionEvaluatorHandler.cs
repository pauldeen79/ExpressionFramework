namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class FieldExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    private readonly IValueProvider _valueProvider;

    public FieldExpressionEvaluatorHandler(IValueProvider valueProvider) => _valueProvider = valueProvider;

    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IFieldExpression fieldExpression)
        {
            return _valueProvider.GetValue(context, fieldExpression.FieldName);
        }

        return Result<object?>.NotSupported();
    }
}
