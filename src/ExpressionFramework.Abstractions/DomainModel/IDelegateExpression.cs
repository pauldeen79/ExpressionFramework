namespace ExpressionFramework.Abstractions.DomainModel;

public interface IDelegateExpression : IExpression
{
    Func<IDelegateExpressionRequest, IDelegateExpressionResponse> ValueDelegate { get; }
}

public interface IDelegateExpressionRequest
{
    object? Context { get; }
    IExpression Expression { get; }
    IExpressionEvaluator Evaluator { get; }
}

public interface IDelegateExpressionResponse
{
    object? Result { get; }
}
