namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITodayExpression : IExpression, ITypedExpression<DateTime>
{
    IDateTimeProvider? DateTimeProvider { get; }
}
