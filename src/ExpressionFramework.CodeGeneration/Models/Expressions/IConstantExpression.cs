namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a constant value")]
public interface IConstantExpression : IExpression
{
    [Description("Value to use")] object? Value { get; }
}
