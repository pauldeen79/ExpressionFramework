namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ContextRequiredAttribute : Attribute
{
    public bool Required { get; }

    public ContextRequiredAttribute(bool required) => Required = required;
}
