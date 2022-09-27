namespace ExpressionFramework.CodeGeneration.Models;

public interface IOperatorDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    string Description { get; }
    bool UsesLeftValue { get; }
    bool UsesRightValue { get; }
    [Required]
    IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
