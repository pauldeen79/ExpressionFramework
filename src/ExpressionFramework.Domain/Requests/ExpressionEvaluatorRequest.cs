namespace ExpressionFramework.Domain.Requests;

public record ExpressionEvaluatorRequest
{
    public ExpressionEvaluatorRequest(object? context, IExpressionEvaluator evaluator)
    {
        Context = context;
        Evaluator = evaluator;
    }

    public object? Context { get; }
    public IExpressionEvaluator Evaluator { get; }
}
