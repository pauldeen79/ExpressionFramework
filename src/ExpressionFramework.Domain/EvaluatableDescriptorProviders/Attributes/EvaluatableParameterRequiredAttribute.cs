namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class EvaluatableParameterRequiredAttribute : Attribute
{
    public string Name { get; }
    public bool Required { get; }

    public EvaluatableParameterRequiredAttribute(string name, bool required)
    {
        Name = name;
        Required = required;
    }
}
