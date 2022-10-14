namespace ExpressionFramework.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ReturnValueAttribute : Attribute
{
    public ResultStatus Status { get; }
    public string Value { get; }
    public Type? ValueType { get; }
    public string Description { get; }

    public ReturnValueAttribute(ResultStatus status, Type valueType, string value, string description)
    {
        Status = status;
        ValueType = valueType;
        Value = value;
        Description = description;
    }

    public ReturnValueAttribute(ResultStatus status, string value, string description)
    {
        Status = status;
        Value = value;
        Description = description;
    }
}
