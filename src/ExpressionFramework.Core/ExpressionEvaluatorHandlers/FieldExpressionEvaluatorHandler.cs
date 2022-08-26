namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class FieldExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    private readonly IValueProvider _valueProvider;

    public FieldExpressionEvaluatorHandler(IValueProvider valueProvider) => _valueProvider = valueProvider;

    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IFieldExpression fieldExpression)
        {
            return Result<object?>.Success(_valueProvider.GetValue(item, fieldExpression.FieldName));
        }

        return Result<object?>.NotSupported();
    }
}
