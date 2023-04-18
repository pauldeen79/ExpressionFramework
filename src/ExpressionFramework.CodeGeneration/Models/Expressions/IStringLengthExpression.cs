namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringLengthExpression : IExpression, ITypedExpression<int>
{
    [Required]
    IExpression Expression { get; }
}
