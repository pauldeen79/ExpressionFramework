namespace CodeGenerationNext.Models.Expressions;

public interface IAggregateFunctionResultValue
{
    object? Value { get; }
    bool Continue { get; }
}
