namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("This expression always returns the default value for the specified type")]
public interface IDefaultExpression<T> : IExpression, ITypedExpression<T>
{
}
