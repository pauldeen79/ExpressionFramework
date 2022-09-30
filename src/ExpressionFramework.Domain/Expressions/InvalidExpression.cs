namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an invalid result")]
[ExpressionUsesContext(false)]
[ParameterDescription(nameof(ErrorMessage), "Error message to use")]
[ParameterRequired(nameof(ErrorMessage), true)]
[ParameterDescription(nameof(ValidationErrors), "Validation errors to use")]
[ParameterRequired(nameof(ValidationErrors), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will always be returned")]
public partial record InvalidExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Invalid(ErrorMessage, ValidationErrors);
}

