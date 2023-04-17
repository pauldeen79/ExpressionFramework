namespace ExpressionFramework.Domain.Tests.Support;

public record TypedErrorConstantExpression<T> : ITypedExpression<T>
{
    private readonly string _message;

    public TypedErrorConstantExpression(string message)
    {
        _message = message;
    }

    public Result<T> EvaluateTyped(object? context)
    => Result<T>.Error("Kaboom");

    public Expression ToUntyped() => new ErrorExpression(_message);
}
