namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class EvaluatableReturnValueAttribute : Attribute
{
    public ResultStatus Status { get; }
    public string Value { get; }
    public string Description { get; }

    public EvaluatableReturnValueAttribute(ResultStatus status, string value, string description)
    {
        Status = status;
        Value = value;
        Description = description;
    }
}
