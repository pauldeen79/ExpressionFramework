namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the length of the (string) expression")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the length for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(int), "The length of the (string) expression", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record StringLengthExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<int> EvaluateTyped(object? context)
        => Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? Result.Success(result.Value!.Length)
                : Result.FromExistingResult<int>(result));
}
