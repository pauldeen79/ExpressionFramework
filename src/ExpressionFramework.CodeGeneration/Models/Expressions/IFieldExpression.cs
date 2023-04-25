namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    ITypedExpression<string> FieldNameExpression { get; }
}
