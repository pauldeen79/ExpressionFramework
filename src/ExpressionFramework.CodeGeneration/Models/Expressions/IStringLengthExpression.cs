namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringLengthExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
}
