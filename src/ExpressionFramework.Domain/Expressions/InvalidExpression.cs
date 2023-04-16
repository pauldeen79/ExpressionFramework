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
        var errorMessageResult = ErrorMessageExpression.Evaluate(context);
        if (!errorMessageResult.IsSuccessful())
        {
            return errorMessageResult;
        }
        
        if (errorMessageResult.Value is not string errorMessage)
        {
            return Result<object?>.Invalid();
        }

        if (!ValidationErrorExpressions.Any())
        {
            return Result<object?>.Invalid(errorMessage);
        }

        var validationErrorResult = ValidationErrorExpressions.EvaluateTypedUntilFirstError<ValidationError>(context, "ValidationErrorExpressions must be a collection of type string");
        if (!validationErrorResult.Last().IsSuccessful())
        {
            return Result<object?>.FromExistingResult(validationErrorResult.Last());
        }

        return Result<object?>.Invalid(errorMessage, validationErrorResult.Select(x => x.Value!));
    }

    public InvalidExpression(Expression errorMessageExpression) : this(errorMessageExpression, Enumerable.Empty<Expression>())
    {
    }
    public InvalidExpression(object? errorMessageExpression, IEnumerable<object?> validationErrorExpressions) : this(new ConstantExpression(errorMessageExpression), validationErrorExpressions.Select(x => new ConstantExpression(x))) { }
    public InvalidExpression(Func<object?, object?> errorMessageExpression, IEnumerable<Func<object?, object?>> validationErrorExpressions) : this(new DelegateExpression(errorMessageExpression), validationErrorExpressions.Select(x => new DelegateExpression(x))) { }
}

public partial record InvalidExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
