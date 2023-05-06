namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INowExpression : IExpression, ITypedExpression<DateTime>
{
    IDateTimeProvider? DateTimeProvider { get; }
}
