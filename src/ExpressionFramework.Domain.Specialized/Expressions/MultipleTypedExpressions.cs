namespace ExpressionFramework.Domain.Expressions;

public partial record MultipleTypedExpressions<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Expressions);

    public Result<IEnumerable<T?>> EvaluateTyped(object? context)
        => Result<IEnumerable<T?>>.Success(Expressions);
}

