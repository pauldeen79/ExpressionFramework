namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("This expression returns the value of the source expression cast to the specified type, or the default value when this is not possible")]
public interface ITryCastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject][Description("Expression to cast")] IExpression SourceExpression { get; }
    [ValidateObject][Description("Value to use, in case the expression could not be cast")] ITypedExpression<T>? DefaultExpression { get; }
}
