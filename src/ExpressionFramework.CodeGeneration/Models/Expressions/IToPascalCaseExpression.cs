namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToPascalCaseExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
}
