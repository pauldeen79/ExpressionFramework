namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an error result")]
[UsesContext(false)]
[ParameterDescription(nameof(ErrorMessageExpression), "Error message to use")]
[ParameterRequired(nameof(ErrorMessageExpression), true)]
[ReturnValue(ResultStatus.Error, "Empty", "This result will be returned when error message expression evaluation succeeds")]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will be returned when error message expression evaluation fails, or its result value is not a string")]
public partial record ErrorExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var errorMessageResult = ErrorMessageExpression.Evaluate(context);
        if (!errorMessageResult.IsSuccessful())
        {
            return errorMessageResult;
        }

        if (errorMessageResult.Value is not string errorMessage)
        {
            return Result<object?>.Invalid("ErrorMessageExpression did not return a string");
        }

        return Result<object?>.Error(errorMessage);
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        var errorMessageResult = ErrorMessageExpression.Evaluate(context);
        if (errorMessageResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"ErrorMessageExpression returned an invalid result. Error message: {errorMessageResult.ErrorMessage}");
        }
        else if (errorMessageResult.Status == ResultStatus.Ok && errorMessageResult.Value is not string fieldName)
        {
            yield return new ValidationResult($"ErrorMessageExpression did not return a string");
        }
    }

    public ErrorExpression(string errorMessage) : this(new ConstantExpression(errorMessage)) { }
}
