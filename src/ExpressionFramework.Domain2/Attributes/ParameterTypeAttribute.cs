namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ParameterTypeAttribute : Attribute
{
    public string Name { get; }
    public Type Type { get; }

    public ParameterTypeAttribute(string name, Type type)
    {
        Name = name;
        Type = type;
    }
}
