namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpressionContextTypeAttribute : Attribute
{
    public Type Type { get; }

    public ExpressionContextTypeAttribute(Type type)
        => Type = type;
}
