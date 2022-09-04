namespace CodeGenerationNext.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required]
    string FieldName { get; }
}
