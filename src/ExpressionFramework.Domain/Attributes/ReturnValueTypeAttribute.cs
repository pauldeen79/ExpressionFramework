namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ReturnValueTypeAttribute : Attribute
{
    public Type Type { get; }

    public ReturnValueTypeAttribute(Type type)
        => Type = type;
}
