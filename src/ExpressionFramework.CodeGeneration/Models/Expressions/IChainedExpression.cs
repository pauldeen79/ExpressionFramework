namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Chains the result of an expression onto the next one, and so on")]
public interface IChainedExpression : IExpression
{
    [Required][ValidateObject][Description("Expressions to use on chaining. The context is chained to the first expression.")] IReadOnlyCollection <IExpression> Expressions { get; }
}
