namespace ExpressionFramework.CodeGeneration.Models;

public interface IExpressionDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    string Description { get; }
    string? ContextTypeName { get; }
    bool ContextIsRequired { get; }
    [Required]
    IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required]
    IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
