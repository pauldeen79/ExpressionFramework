namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to lower case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the lower case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to lower case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToLowerCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? Result.Success(result.Value!.ToLower())
                : result);
}
