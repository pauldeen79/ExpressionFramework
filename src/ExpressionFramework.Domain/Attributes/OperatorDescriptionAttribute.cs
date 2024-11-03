namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OperatorDescriptionAttribute : DescriptionBaseAttribute
{
    public OperatorDescriptionAttribute(string description) : base(description)
    {
    }
}
