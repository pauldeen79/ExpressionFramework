namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ExpressionNameAttribute : System.Attribute
{
    public string Name { get; }
    public string Namespace { get; }

    public ExpressionNameAttribute(string name, string @namespace)
    {
        ArgumentGuard.IsNotNull(name, nameof(name));
        ArgumentGuard.IsNotNull(@namespace, nameof(@namespace));

        Name = name;
        Namespace = @namespace;
    }

    public ExpressionNameAttribute(string name) : this(name, string.Empty)
    {
    }
}
