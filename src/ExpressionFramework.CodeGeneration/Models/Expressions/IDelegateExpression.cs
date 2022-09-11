namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    Func<IDelegateExpressionRequest, object?> Value { get; }
}

