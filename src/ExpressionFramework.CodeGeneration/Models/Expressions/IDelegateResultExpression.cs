namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a result from a delegate")]
public interface IDelegateResultExpression : IExpression
{
    [Required][Description("Delegate to use")] Func<object?, Result<object?>> Result { get; }
}
