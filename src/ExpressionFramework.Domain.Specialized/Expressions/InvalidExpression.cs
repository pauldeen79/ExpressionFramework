namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an invalid result")]
[ParameterDescription(nameof(ErrorMessageExpression), "Error message to use (may be empty)")]
[ParameterRequired(nameof(ErrorMessageExpression), true)]
[ParameterDescription(nameof(ValidationErrorExpressions), "Validation errors to use")]
[ParameterRequired(nameof(ValidationErrorExpressions), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will always be returned")]
public partial record InvalidExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var errorMessageResult = ErrorMessageExpression.EvaluateTyped(context);
        if (!errorMessageResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(errorMessageResult);
        }
        
        if (errorMessageResult.Value is not string errorMessage)
        {
            return Result<object?>.Invalid();
        }

        if (ValidationErrorExpressions is null)
        {
            return Result<object?>.Invalid(errorMessage);
        }

        var validationErrorExpressionsResult = ValidationErrorExpressions.EvaluateTyped(context);
        if (!validationErrorExpressionsResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(validationErrorExpressionsResult);
        }

        return validationErrorExpressionsResult.Value is null || !validationErrorExpressionsResult.Value.Any()
            ? Result<object?>.Invalid(errorMessage)
            : Result<object?>.Invalid(errorMessage, validationErrorExpressionsResult.Value!);
    }

    public InvalidExpression(string errorMessageExpression = "", IEnumerable<ValidationError>? validationErrorExpressions = null) : this(new TypedConstantExpression<string>(errorMessageExpression), validationErrorExpressions == null ? null : new MultipleTypedExpressions<ValidationError>(validationErrorExpressions)) { }
}
