namespace ExpressionFramework.CodeGeneration.Models;

public interface IAggregatorDescriptor
{
    [Required] string Name { get; }
    [Required] string TypeName { get; }
    string Description { get; }
    string ContextTypeName { get; }
    string ContextDescription { get; }
    [Required][ValidateObject] IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required][ValidateObject] IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
