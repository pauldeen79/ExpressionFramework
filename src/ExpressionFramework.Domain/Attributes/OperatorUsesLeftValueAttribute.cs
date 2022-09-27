namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OperatorUsesLeftValueAttribute : Attribute
{
    public bool UsesLeftValue { get; }

    public OperatorUsesLeftValueAttribute(bool usesLeftValue) => UsesLeftValue = usesLeftValue;
}
