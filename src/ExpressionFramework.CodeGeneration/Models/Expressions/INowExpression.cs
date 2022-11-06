namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INowExpression : IExpression
{
    IDateTimeProvider? DateTimeProvider { get; }
}
