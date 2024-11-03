namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ExpressionDescriptionAttribute : DescriptionBaseAttribute
{
    public ExpressionDescriptionAttribute(string description) : base(description)
    {
    }
}
