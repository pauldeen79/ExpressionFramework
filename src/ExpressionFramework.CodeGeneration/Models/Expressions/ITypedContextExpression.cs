namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the typed value of the context")]
public interface ITypedContextExpression<T> : IExpression, ITypedExpression<T>
{
}
