namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of a field (property) of the context")]
[ParameterDescription(nameof(FieldNameExpression), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldNameExpression), true)]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Object to get the value from")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Fieldname [x] is not found on type [y]")]
[ReturnValue(ResultStatus.Ok, "Value of the field (property)", "This will be returned if the field (property) is found")]
public partial record FieldExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var fieldNameResult = FieldNameExpression.Evaluate(context);
        if (!fieldNameResult.IsSuccessful())
        {
            return fieldNameResult;
        }

        if (fieldNameResult.Value is not string fieldName)
        {
            return Result<object?>.Invalid("FieldNameExpression did not return a string");
        }

        return GetValue(context, fieldName);
    }

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

        var localFieldName = string.Empty;
        var fieldNameResult = FieldNameExpression.Evaluate(context);
        if (fieldNameResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"FieldNameExpression returned an invalid result. Error message: {fieldNameResult.ErrorMessage}");
        }
        else if (fieldNameResult.Status == ResultStatus.Ok)
        {
            if (fieldNameResult.Value is not string fieldName)
            {
                yield return new ValidationResult($"FieldNameExpression did not return a string");
            }
            else
            {
                localFieldName = fieldName;
            }
        }

        var type = context.GetType();
        foreach (var part in localFieldName.Split('.'))
        {
            var property = type.GetProperty(part);

            if (property == null)
            {
                yield return new ValidationResult($"Fieldname [{localFieldName}] is not found on type [{type.FullName}]");
                continue;
            }

            var returnValue = property.GetValue(context);
            if (returnValue == null)
            {
                break;
            }
            context = returnValue;
            type = returnValue.GetType();
        }
    }
}
