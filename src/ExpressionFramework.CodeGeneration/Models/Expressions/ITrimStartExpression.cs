namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimStartExpression : IExpression
{
    IReadOnlyCollection<char>? TrimChars { get; }
}
