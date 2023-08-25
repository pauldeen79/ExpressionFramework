namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Concatenates strings")]
[UsesContext(true)]
[ContextDescription("Value to use as context in expression evaluation")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Expressions), "Strings to concatenate")]
[ParameterType(nameof(Expressions), typeof(string))]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(string), "The concatenated string", "This result will be returned when the expressions are all of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "At least one expression is required, Expressions must be of type string")]
public partial record StringConcatenateExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
    {
        var valueResults = Expressions.EvaluateTypedUntilFirstError(context, "Expressions must be of type string");
        if (valueResults.Length == 0)
        {
            return Result<string>.Invalid("At least one expression is required");
        }

        if (!valueResults[valueResults.Length - 1].IsSuccessful())
        {
            return Result<string>.FromExistingResult(valueResults[valueResults.Length - 1]);
        }

        return Result<string>.Success(string.Concat(valueResults.Select(x => x.Value)));
    }
}
