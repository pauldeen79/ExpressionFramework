namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a typed result constant value")]
public interface ITypedConstantResultExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][Description("Value to use")] Result <T> Value { get; }
}
