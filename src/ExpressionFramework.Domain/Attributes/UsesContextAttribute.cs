namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class UsesContextAttribute : Attribute
{
    public bool UsesContext { get; }

    public UsesContextAttribute(bool usesContext) => UsesContext = usesContext;
}
