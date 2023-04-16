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
        => ErrorMessageExpression.EvaluateTyped<string>(context, "ErrorMessageExpression did not return a string").Transform(result =>
            result.IsSuccessful()
                ? Result<object?>.Error(result.Value!)
                : Result<object?>.FromExistingResult(result));

    public ErrorExpression(object? errorMessageExpression) : this(new ConstantExpression(errorMessageExpression)) { }
    public ErrorExpression(Func<object?, object?> errorMessageExpression) : this(new DelegateExpression(errorMessageExpression)) { }
}

public partial record ErrorExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
