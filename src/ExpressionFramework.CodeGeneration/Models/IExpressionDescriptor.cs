namespace ExpressionFramework.CodeGeneration.Models;

public interface IExpressionDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    string Description { get; }
    bool UsesContext { get; }
    string? ContextTypeName { get; }
    string? ContextDescription { get; }
    bool? ContextIsRequired { get; }
    [Required]
    IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required]
    IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
