namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("This expression always returns the default value for the specified type")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, "Default value of the specified type", "This result will always be returned")]
public partial record DefaultExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(default(T));
}

#pragma warning disable S2326 // Unused type parameters should be removed
public partial record DefaultExpressionBase<T>
#pragma warning restore S2326 // Unused type parameters should be removed
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
