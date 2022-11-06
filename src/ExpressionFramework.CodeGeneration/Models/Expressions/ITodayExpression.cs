namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITodayExpression : IExpression
{
    IDateTimeProvider? DateTimeProvider { get; }
}
