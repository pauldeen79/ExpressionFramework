namespace ExpressionFramework.Domain.Expressions;

public partial record FieldExpression
{
    public override Result<object?> Evaluate(object? context)
        => GetValue(context, FieldName);

    private Result<object?> GetValue(object? context, string fieldName)
    {
        if (context == null)
        {
            return Result<object?>.Invalid("Context cannot be empty");
        }

        var type = context.GetType();
        object? returnValue = null;
        foreach (var part in fieldName.Split('.'))
        {
            var property = type.GetProperty(part);

            if (property == null)
            {
                return Result<object?>.Invalid($"Fieldname [{fieldName}] is not found on type [{type.FullName}]");
            }

            returnValue = property.GetValue(context);
            if (returnValue == null)
            {
                break;
            }
            context = returnValue;
            type = returnValue.GetType();
        }

        return Result<object?>.Success(returnValue);
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
        }
    }
}
