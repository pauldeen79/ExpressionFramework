namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpressionDescriptionAttribute : Attribute
{
    public string Description { get; }

    public ExpressionDescriptionAttribute(string description)
        => Description = description;
}
