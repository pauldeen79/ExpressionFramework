namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ReturnValueAttribute : Attribute
{
    public ResultStatus Status { get; }
    public string Value { get; }
    public string Description { get; }

    public ReturnValueAttribute(ResultStatus status, string value, string description)
    {
        Status = status;
        Value = value;
        Description = description;
    }
}
