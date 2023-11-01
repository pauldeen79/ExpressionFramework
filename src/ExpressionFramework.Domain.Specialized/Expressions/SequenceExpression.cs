namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns all supplied expressions into a sequence (enumerable)")]
[UsesContext(false)]
[ParameterDescription(nameof(Expressions), "Expressions to put in a sequence (enumerable)")]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Enumerable with items", "This will be returned in case all expressions return Ok")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case any expression returns something else than Ok")]
public partial record SequenceExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var values = Expressions.EvaluateUntilFirstError(context);
        if (values.Any() && !values[values.Length - 1].IsSuccessful())
        {
            return values[values.Length - 1];
        }

        return Result.Success<object?>(values.Select(x => x.Value));
    }

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
    {
        var values = Expressions.EvaluateUntilFirstError(context);
        if (values.Any() && !values[values.Length - 1].IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(values[values.Length - 1]);
        }

        return Result.Success(values.Select(x => x.Value));
    }
}
