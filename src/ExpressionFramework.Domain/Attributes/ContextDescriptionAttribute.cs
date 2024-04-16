namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ContextDescriptionAttribute : Attribute
{
    public string Description { get; }

    public ContextDescriptionAttribute(string description)
        => Description = description;
}
