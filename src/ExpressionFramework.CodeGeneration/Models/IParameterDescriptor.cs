namespace ExpressionFramework.CodeGeneration.Models;

public interface IParameterDescriptor
{
    [Required]
    string Name { get; }
    [Required]
    string TypeName { get; }
    [Required]
    string Description { get; }
    bool Required { get; }
}
