namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OperatorLeftValueTypeAttribute : Attribute
{
    public Type Type { get; }

    public OperatorLeftValueTypeAttribute(Type type)
        => Type = type;
}
