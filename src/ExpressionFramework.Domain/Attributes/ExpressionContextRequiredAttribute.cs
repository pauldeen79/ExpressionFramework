namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpressionContextRequiredAttribute : Attribute
{
    public bool Required { get; }

    public ExpressionContextRequiredAttribute(bool required) => Required = required;
}
