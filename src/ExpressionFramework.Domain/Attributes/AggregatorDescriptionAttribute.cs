namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class AggregatorDescriptionAttribute : DescriptionBaseAttribute
{
    public AggregatorDescriptionAttribute(string description) : base(description)
    {
    }
}
