namespace ExpressionFramework.CodeGeneration.Models;

public interface IParameterDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    string Description { get; }
    bool Required { get; }
}
