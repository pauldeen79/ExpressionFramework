namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("This expression returns the value of the source expression cast to the specified type")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, "Value cast to specified type, when possible", "This result will be returned when the source expression can be cast to the specified type")]
[ReturnValue(ResultStatus.Invalid, "Empty", "SourceExpression is not of type x")]
public partial record CastExpression<T>
{
    public override Result<object?> Evaluate(object? context) => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<T> EvaluateTyped(object? context)
    {
        var result = SourceExpression.Evaluate(context).TryCast<T>($"SourceExpression is not of type {typeof(T).FullName}");

        if (result.IsSuccessful() && result.GetValue() is null)
        {
            //HACK: Null values now work differently with TryCast. We need a new method on Result to fix this... For now, do a work-around.
            return Result.Invalid<T>($"SourceExpression is not of type {typeof(T).FullName}");
        }

        return result;
    }

    public Expression ToUntyped() => SourceExpression;
}
