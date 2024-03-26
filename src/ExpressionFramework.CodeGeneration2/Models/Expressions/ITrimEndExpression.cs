namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimEndExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [ValidateObject] ITypedExpression<char[]>? TrimCharsExpression { get; }
}
