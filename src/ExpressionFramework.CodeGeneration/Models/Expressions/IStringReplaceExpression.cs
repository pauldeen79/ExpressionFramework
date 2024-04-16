namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringReplaceExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<string> FindExpression { get; }
    [Required][ValidateObject] ITypedExpression<string> ReplaceExpression { get; }
}
