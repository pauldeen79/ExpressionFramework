namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required]
    string FieldName { get; }
}
