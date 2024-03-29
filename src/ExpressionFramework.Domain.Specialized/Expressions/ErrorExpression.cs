﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an error result")]
[UsesContext(false)]
[ParameterDescription(nameof(ErrorMessageExpression), "Error message to use")]
[ParameterRequired(nameof(ErrorMessageExpression), true)]
[ReturnValue(ResultStatus.Error, "Empty", "This result will be returned when error message expression evaluation succeeds")]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will be returned when error message expression evaluation fails, or its result value is not a string")]
public partial record ErrorExpression
{
    public override Result<object?> Evaluate(object? context)
        => ErrorMessageExpression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? Result.Error<object?>(result.Value!)
                : Result.FromExistingResult<object?>(result));
}
