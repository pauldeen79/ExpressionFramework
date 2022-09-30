namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OperatorRightValueTypeAttribute : Attribute
{
    public Type Type { get; }

    public OperatorRightValueTypeAttribute(Type type)
        => Type = type;
}
