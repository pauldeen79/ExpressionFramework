namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class EvaluatableParameterDescriptionAttribute : Attribute
{
    public string Name { get; }
    public string Description { get; }

    public EvaluatableParameterDescriptionAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
