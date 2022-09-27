namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ParameterDescriptionAttribute : Attribute
{
    public string Name { get; }
    public string Description { get; }

    public ParameterDescriptionAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
