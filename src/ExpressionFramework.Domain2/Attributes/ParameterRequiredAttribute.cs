namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ParameterRequiredAttribute : Attribute
{
    public string Name { get; }
    public bool Required { get; }

    public ParameterRequiredAttribute(string name, bool required)
    {
        Name = name;
        Required = required;
    }
}
