namespace CodeGenerationNext.Models;

public interface IConstantExpression : IExpression
{
    object? Value { get; }
}
