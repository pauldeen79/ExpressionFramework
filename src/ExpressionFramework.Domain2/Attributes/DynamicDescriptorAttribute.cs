namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class DynamicDescriptorAttribute : Attribute
{
    public Type Type { get; }

    public DynamicDescriptorAttribute(Type type)
    {
        Type = type;
    }
}
