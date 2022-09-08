namespace ExpressionFramework.Domain.ExpressionHandlers;

public class FieldExpressionHandler : ExpressionHandlerBase<FieldExpression>
{
    private readonly IValueProvider _valueProvider;

    public FieldExpressionHandler(IValueProvider valueProvider)
        => _valueProvider = valueProvider;

    protected override Task<Result<object?>> Evaluate(object? context, FieldExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(_valueProvider.GetValue(context, typedExpression.FieldName));
}
