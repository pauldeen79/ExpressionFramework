namespace CodeGenerationNext.Models;

public interface IFieldExpression : IExpression
{
    string FieldName { get; }
}
