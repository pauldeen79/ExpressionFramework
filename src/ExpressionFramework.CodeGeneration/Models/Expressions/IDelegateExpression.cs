namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    Func<object?, object?> Value { get; }
}

