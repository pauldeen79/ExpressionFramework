namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateResultExpression : IExpression
{
    [Required] Func<object?, Result<object?>> Result { get; }
}
