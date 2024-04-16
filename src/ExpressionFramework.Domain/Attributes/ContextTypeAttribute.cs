namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ContextTypeAttribute : Attribute
{
    public Type Type { get; }

    public ContextTypeAttribute(Type type)
        => Type = type;
}
