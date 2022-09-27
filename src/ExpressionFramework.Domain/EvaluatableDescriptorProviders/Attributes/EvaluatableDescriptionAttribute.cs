namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class EvaluatableDescriptionAttribute : Attribute
{
    public string Description { get; }

    public EvaluatableDescriptionAttribute(string description)
        => Description = description;
}
