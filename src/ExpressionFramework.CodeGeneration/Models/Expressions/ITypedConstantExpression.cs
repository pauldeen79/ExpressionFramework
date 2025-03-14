namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a typed constant value")]
public interface ITypedConstantExpression<T> : IExpression, ITypedExpression<T>
{
    [Description("Value to use")] T Value { get; }
}
