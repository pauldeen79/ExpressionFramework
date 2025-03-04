namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a typed result value from a typed delegate")]
public interface ITypedDelegateResultExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][Description("Delegate to use")] Func<object?, Result<T>> Value { get; }
}
