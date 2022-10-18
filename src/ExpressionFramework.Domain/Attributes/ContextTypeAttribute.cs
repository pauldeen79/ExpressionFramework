namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ContextTypeAttribute : Attribute
{
    public Type Type { get; }

    public ContextTypeAttribute(Type type)
        => Type = type;
}
