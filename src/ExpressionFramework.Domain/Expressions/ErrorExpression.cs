namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns an error result")]
[ExpressionUsesContext(false)]
[ParameterDescription(nameof(ErrorMessage), "Error message to use")]
[ParameterRequired(nameof(ErrorMessage), true)]
[ReturnValue(ResultStatus.Error, "Empty", "This result will always be returned")]
public partial record ErrorExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Error(ErrorMessage);
}
