namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OperatorLeftValueTypeAttribute : Attribute
{
    public Type Type { get; }

    public OperatorLeftValueTypeAttribute(Type type)
        => Type = type;
}
