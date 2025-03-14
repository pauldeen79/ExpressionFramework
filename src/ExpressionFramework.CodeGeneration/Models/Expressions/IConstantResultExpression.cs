namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns a constant result")]
public interface IConstantResultExpression : IExpression
{
    [Required][ValidateObject][Description("Result to use")] Result Value { get; }
}
