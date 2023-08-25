namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OperatorUsesRightValueAttribute : Attribute
{
    public bool UsesRightValue { get; }

    public OperatorUsesRightValueAttribute(bool usesRightValue) => UsesRightValue = usesRightValue;
}
