namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToLowerCaseExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
}
