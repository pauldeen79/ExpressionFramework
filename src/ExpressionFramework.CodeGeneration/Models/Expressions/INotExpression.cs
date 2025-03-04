namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the inverted value of the boolean value")]
public interface INotExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Boolean to invert")] ITypedExpression<bool> Expression { get; }
}
