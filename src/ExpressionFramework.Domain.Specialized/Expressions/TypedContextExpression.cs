namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the typed value of the context")]
[UsesContext(true)]
[ContextDescription("Context value to use")]
[ContextType(typeof(object))]
[ContextRequired(true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the context", "This result will be returned when the context is of the correct type")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type T")]
public partial record TypedContextExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<T> EvaluateTyped(object? context)
        => context is T t
            ? Result<T>.Success(t)
            : Result<T>.Invalid($"Context is not of type {typeof(T).FullName}");
}

