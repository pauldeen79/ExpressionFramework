namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    [Required] Func<object?, object?> Value { get; }
}

