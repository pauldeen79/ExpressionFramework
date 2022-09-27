namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpressionContextDescriptionAttribute : Attribute
{
    public string Description { get; }

    public ExpressionContextDescriptionAttribute(string description)
        => Description = description;
}
