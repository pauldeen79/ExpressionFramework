namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of a field (property) of the context")]
[ParameterDescription(nameof(FieldNameExpression), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldNameExpression), true)]
[ParameterType(nameof(FieldNameExpression), typeof(string))]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Object to get the value from")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Fieldname [x] is not found on type [y]")]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the field (property)", "This will be returned if the field (property) is found")]
public partial record FieldExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (context == null)
        {
            return Result<object?>.Invalid("Context cannot be empty");
        }

        return FieldNameExpression
            .Evaluate(context)
            .TryCast<string>("FieldNameExpression did not return a string")
            .Transform(fieldNameResult => fieldNameResult.IsSuccessful()
                ? GetValue(context, fieldNameResult.Value)
                : Result<object?>.FromExistingResult(fieldNameResult));
    }

    private Result<object?> GetValue(object context, string? fieldName)
    {
        if (fieldName == null || fieldName.Length == 0)
        {
            return Result<object?>.Invalid("FieldNameExpression returned an empty string");
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
            yield break;
        }

        if (fieldNameResult.Status == ResultStatus.Ok)
        {
            if (fieldNameResult.Value is not string fieldName)
            {
                yield return new ValidationResult($"FieldNameExpression did not return a string");
                yield break;
            }
            else
            {
                localFieldName = fieldName;
            }
        }

        if (string.IsNullOrEmpty(localFieldName))
        {
            yield return new ValidationResult("FieldNameExpression returned an empty string");
            yield break;
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

    public FieldExpression(string fieldName) : this(new ConstantExpression(fieldName)) { }
}
