namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a value from a delegate")]
public interface IDelegateExpression : IExpression
{
    [Required][Description("Delegate to use")] Func<object?, object?> Value { get; }
}
