namespace ExpressionFramework.Abstractions.DomainModel;

public interface IDelegateExpression : IExpression
{
    Func<object?, object?, IExpression, IExpressionEvaluator, object?> ValueDelegate { get; }
}
