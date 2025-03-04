namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the current date")]
public interface ITodayExpression : IExpression, ITypedExpression<DateTime>
{
    [Description("Optional provider for date time to use instead of the system clock")] IDateTimeProvider? DateTimeProvider { get; }
}
