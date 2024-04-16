namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class DescriptionBaseAttribute : Attribute
{
    public string Description { get; }

    protected DescriptionBaseAttribute(string description)
        => Description = description;
}
