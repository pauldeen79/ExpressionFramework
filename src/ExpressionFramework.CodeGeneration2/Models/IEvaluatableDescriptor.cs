namespace ExpressionFramework.CodeGeneration.Models;

public interface IEvaluatableDescriptor
{
    [Required] string Name { get; }
    [Required] string TypeName { get; }
    string Description { get; }
    bool UsesContext { get; }
    string? ContextTypeName { get; }
    string? ContextDescription { get; }
    bool? ContextIsRequired { get; }
    [Required][ValidateObject] IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required][ValidateObject] IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
