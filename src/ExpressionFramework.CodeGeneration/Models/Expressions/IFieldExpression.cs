namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required]
    IExpression FieldNameExpression { get; }
}
