namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class FieldExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IValueProvider _valueProvider;

    public FieldExpressionEvaluatorProvider(IValueProvider valueProvider) => _valueProvider = valueProvider;

    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IFieldExpression fieldExpression)
        {
            return Result<object?>.Success(_valueProvider.GetValue(item, fieldExpression.FieldName));
        }

        return Result<object?>.NotSupported();
    }
}
