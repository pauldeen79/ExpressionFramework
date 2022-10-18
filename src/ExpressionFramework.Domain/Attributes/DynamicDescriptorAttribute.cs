namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DynamicDescriptorAttribute : Attribute
{
    public Type Type { get; }

    public DynamicDescriptorAttribute(Type type)
    {
        Type = type;
    }
}
