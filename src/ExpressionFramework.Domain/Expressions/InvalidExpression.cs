﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an invalid result")]
[ExpressionUsesContext(false)]
[ParameterDescription(nameof(ErrorMessageExpression), "Error message to use")]
[ParameterRequired(nameof(ErrorMessageExpression), true)]
[ParameterDescription(nameof(ValidationErrors), "Validation errors to use")]
[ParameterRequired(nameof(ValidationErrors), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will always be returned")]
public partial record InvalidExpression
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

        return Result<object?>.Invalid(errorMessage, ValidationErrors);
    }

    public InvalidExpression(Expression errorMessageExpression) : this(errorMessageExpression, Enumerable.Empty<ValidationError>())
    {
    }
}

