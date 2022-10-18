namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ContextRequiredAttribute : Attribute
{
    public bool Required { get; }

    public ContextRequiredAttribute(bool required) => Required = required;
}
