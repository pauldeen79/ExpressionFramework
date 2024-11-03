namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EvaluatableDescriptionAttribute : DescriptionBaseAttribute
{
    public EvaluatableDescriptionAttribute(string description) : base(description)
    {
    }
}
