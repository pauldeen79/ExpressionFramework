namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    [CsharpTypeName("System.Func<System.Object?,System.Object?>")]
    [Required] Func<object?, object?> Value { get; }
}

