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
            return Result.FromExistingResult<object?>(errorMessageResult);
        }

        if (errorMessageResult.Value is not string errorMessage)
        {
            return Result.Invalid<object?>();
        }

        if (ValidationErrorExpressions.Count == 0)
        {
            return Result.Invalid<object?>(errorMessage);
        }

        var validationErrorResult = ValidationErrorExpressions.EvaluateTypedUntilFirstError(context, "ValidationErrorExpressions must be a collection of type string");
        if (!validationErrorResult[validationErrorResult.Length - 1].IsSuccessful())
        {
            return Result.FromExistingResult<object?>(validationErrorResult[validationErrorResult.Length - 1]);
        }

        return Result.Invalid<object?>(errorMessage, validationErrorResult.Select(x => x.Value!));
    }
}
