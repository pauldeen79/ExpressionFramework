namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimEndExpression : IExpression
{
    IReadOnlyCollection<char>? TrimChars { get; }
}
