namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of a field (property) of the context")]
[ParameterDescription(nameof(FieldName), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldName), true)]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Object to get the value from")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Fieldname [x] is not found on type [y]")]
[ReturnValue(ResultStatus.Ok, "Value of the field (property)", "This will be returned if the field (property) is found")]
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
            yield break;
        }

        var type = context.GetType();
        object? returnValue = null;
        foreach (var part in FieldName.Split('.'))
        {
            var property = type.GetProperty(part);

            if (property == null)
            {
                yield return new ValidationResult($"Fieldname [{FieldName}] is not found on type [{type.FullName}]");
                continue;
            }

            returnValue = property.GetValue(context);
            if (returnValue == null)
            {
                break;
            }
            context = returnValue;
            type = returnValue.GetType();
        }
    }
}
