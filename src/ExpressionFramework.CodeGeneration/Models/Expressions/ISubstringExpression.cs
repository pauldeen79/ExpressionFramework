namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISubstringExpression : IExpression
{
    int Index { get; }
    int Length { get; }
}
