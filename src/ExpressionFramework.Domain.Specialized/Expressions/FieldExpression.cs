namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of a field (property) of the context")]
[ParameterDescription(nameof(FieldNameExpression), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldNameExpression), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Fieldname [x] is not found on type [y]")]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the field (property)", "This will be returned if the field (property) is found")]
public partial record FieldExpression
{
    public override Result<object?> Evaluate(object? context) => Evaluate(context, Expression, FieldNameExpression);

    internal static Result<object?> Evaluate(object? context, Expression expression, ITypedExpression<string> fieldNameExpression)
    {
        var result = expression.EvaluateWithNullCheck(context);
        if (!result.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(result);
        }

        var fieldNameResult = fieldNameExpression.EvaluateTyped(result.Value);
        if (!fieldNameResult.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(fieldNameResult);
        }

        if (string.IsNullOrEmpty(fieldNameResult.Value))
        {
            return Result.Invalid<object?>("FieldNameExpression must be a non empty string");
        }

        return GetValue(result.Value!, fieldNameResult.Value!);
    }

    private static Result<object?> GetValue(object value, string fieldName)
    {
        var type = value.GetType();
        object? returnValue = null;
        foreach (var part in fieldName.Split('.'))
        {
            var property = type.GetProperty(part);

            if (property is null)
            {
                return Result.Invalid<object?>($"Fieldname [{fieldName}] is not found on type [{type.FullName}]");
            }

            returnValue = property.GetValue(value);
            if (returnValue is null)
            {
                break;
            }
            value = returnValue;
            type = returnValue.GetType();
        }

        return Result.Success<object?>(returnValue);
    }
}
