namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("This expression returns the value of the source expression cast to the specified type")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, "Value cast to specified type, when possible", "This result will be returned when the source expression can be cast to the specified type")]
[ReturnValue(ResultStatus.Invalid, "Empty", "SourceExpression is not of type x")]
public partial record TryCastExpression<T>
{
    public override Result<object?> Evaluate(object? context) => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<T> EvaluateTyped(object? context)
    {
        var sourceResult = SourceExpression.Evaluate(context);
        if (!sourceResult.IsSuccessful())
        {
            return Result.FromExistingResult<T>(sourceResult);
        }

        if (sourceResult.Value is T t)
        {
            return Result.Success(t);
        }

        if (DefaultExpression is null)
        {
            return Result.Success<T>(default!);
        }

        return DefaultExpression.EvaluateTyped(context);
    }

    public Expression ToUntyped() => SourceExpression;
}
