namespace CodeGenerationNext.Models.Expressions;

public interface IFieldExpression : IExpression
{
    string FieldName { get; }
}
