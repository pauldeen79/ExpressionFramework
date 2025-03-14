namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a value from a typed delegate")]
public interface ITypedDelegateExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][Description("Delegate to use")] Func<object?, T> Value { get; }
}
