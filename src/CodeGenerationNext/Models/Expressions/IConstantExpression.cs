namespace CodeGenerationNext.Models.Expressions;

public interface IConstantExpression : IExpression
{
    object? Value { get; }
}
