namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimExpression : IExpression
{
    IReadOnlyCollection<char>? TrimChars { get; }
}
