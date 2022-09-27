namespace ExpressionFramework.CodeGeneration.Models;

public interface IReturnValueDescriptor
{
    ResultStatus Status { get; }
    [Required]
    string Value { get; }
    string Description { get; }
}
