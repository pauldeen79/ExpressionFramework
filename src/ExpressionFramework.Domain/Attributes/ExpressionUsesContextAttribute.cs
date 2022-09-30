namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpressionUsesContextAttribute : Attribute
{
    public bool UsesContext { get; }

    public ExpressionUsesContextAttribute(bool usesContext) => UsesContext = usesContext;
}
