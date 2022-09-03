namespace CodeGenerationNext.Models;

public interface IAggregateFunctionResultValue
{
    object? Value { get; }
    bool Continue { get; }
}
