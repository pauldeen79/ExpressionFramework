﻿namespace ExpressionFramework.Domain.Expressions;

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

        if (!ValidationErrorExpressions.Any())
        {
            return Result<object?>.Invalid(errorMessage);
        }

        var validationErrorResult = ValidationErrorExpressions.EvaluateTypedUntilFirstError(context, "ValidationErrorExpressions must be a collection of type string");
        if (!validationErrorResult.Last().IsSuccessful())
        {
            return Result<object?>.FromExistingResult(validationErrorResult.Last());
        }

        return Result<object?>.Invalid(errorMessage, validationErrorResult.Select(x => x.Value!));
    }

    public InvalidExpression(string errorMessageExpression = "", IEnumerable<ValidationError>? validationErrorExpressions = null) : this(new TypedConstantExpression<string>(errorMessageExpression), validationErrorExpressions == null ? Enumerable.Empty<ITypedExpression<ValidationError>>() : validationErrorExpressions.Select(x => new TypedConstantExpression<ValidationError>(x))) { }
}
