namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the typed value of a field (property) of the context")]
[ParameterDescription(nameof(FieldNameExpression), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldNameExpression), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Fieldname [x] is not found on type [y], Field is not of type [z]")]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the field (property)", "This will be returned if the field (property) is found")]
public partial record TypedFieldExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Expression ToUntyped() => new FieldExpression(Expression, FieldNameExpression);

    public Result<T> EvaluateTyped(object? context)
    {
        var untypedResult = FieldExpression.Evaluate(context, Expression, FieldNameExpression);
        return untypedResult.IsSuccessful()
            ? untypedResult.TryCast<T>($"Field is not of type [{typeof(T).FullName}]")
            : Result<T>.FromExistingResult(untypedResult);
    }
}
