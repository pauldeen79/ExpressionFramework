namespace ExpressionFramework.CodeGeneration.Attributes;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public sealed class ExpressionNameAttribute : System.Attribute
{
    public string Name { get; }
    public string Namespace { get; }

    public ExpressionNameAttribute(string name, string @namespace)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(@namespace);

        Name = name;
        Namespace = @namespace;
    }

    public ExpressionNameAttribute(string name) : this(name, string.Empty)
    {
    }
}
