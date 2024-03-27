namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IConstantResultExpression : IExpression
{
    [Required][ValidateObject] Result Value { get; }
}
