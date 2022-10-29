namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression FieldNameExpression { get; }
}
