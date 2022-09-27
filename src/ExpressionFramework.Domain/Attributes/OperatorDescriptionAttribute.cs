namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OperatorDescriptionAttribute : Attribute
{
    public string Description { get; }

    public OperatorDescriptionAttribute(string description)
        => Description = description;
}
