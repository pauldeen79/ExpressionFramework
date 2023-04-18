namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value on type")]
[ContextDescription("Value to use as context in the expression")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(TypeExpression), "Type to filter on")]
[ParameterRequired(nameof(TypeExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that are of the specified type", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type enumerable")]
public partial record OfTypeExpression : ITypedExpression<IEnumerable<object?>>
{
    public override Result<object?> Evaluate(object? context) => Result<object?>.FromExistingResult(EvaluateTyped(context), result => result);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
    {
        var typeResult = TypeExpression.EvaluateTyped<Type>(context, "TypeExpression is not of type Type");
        if (!typeResult.IsSuccessful())
        {
            return Result< IEnumerable<object?>>.FromExistingResult(typeResult);
        }

        return EnumerableExpression.GetTypedResultFromEnumerable(Expression, context, e => IsOfType(e, typeResult));
    }

    public Expression ToUntyped() => this;

    public OfTypeExpression(object? expression, Expression typeExpression) : this(new ConstantExpression(expression), typeExpression) { }
    public OfTypeExpression(Func<object?, object?> expression, Expression typeExpression) : this(new DelegateExpression(expression), typeExpression) { }

    private static IEnumerable<Result<object?>> IsOfType(IEnumerable<object?> e, Result<Type> typeResult) => e
        .Where(x => x != null && typeResult.Value!.IsInstanceOfType(x))
        .Select(x => Result<object?>.Success(x));
}

public partial record OfTypeExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
