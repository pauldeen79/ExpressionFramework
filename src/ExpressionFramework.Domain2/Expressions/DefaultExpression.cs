namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("This expression always returns the default value for the specified type")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, "Default value of the specified type", "This result will always be returned")]
public partial record DefaultExpression<T>
{
    public override Result<object?> Evaluate(object? context) => Result.Success<object?>(default(T));

    public Result<T> EvaluateTyped(object? context) => Result.Success<T>(default!);

    public Expression ToUntyped() => new ConstantExpression(default(T));
}
