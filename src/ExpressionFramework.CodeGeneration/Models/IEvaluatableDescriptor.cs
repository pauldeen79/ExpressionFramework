namespace ExpressionFramework.CodeGeneration.Models;

public interface IEvaluatableDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    [Required]
    string Description { get; }
    [Required]
    IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required]
    IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
