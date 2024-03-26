namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringFindExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<string> FindExpression { get; }
}
