namespace ExpressionFramework.CodeGeneration.Models;

public interface IOperatorDescriptor
{
    [Required] string Name { get; }
    [Required] string TypeName { get; }
    string Description { get; }
    bool UsesLeftValue { get; }
    string? LeftValueTypeName { get; }
    bool UsesRightValue { get; }
    string? RightValueTypeName { get; }
    [Required][ValidateObject] IReadOnlyCollection<IParameterDescriptor> Parameters { get; }
    [Required][ValidateObject] IReadOnlyCollection<IReturnValueDescriptor> ReturnValues { get; }
}
