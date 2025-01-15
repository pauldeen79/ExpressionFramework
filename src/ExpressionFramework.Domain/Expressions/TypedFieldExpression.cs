namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the typed value of a field (property) of the context")]
[ParameterDescription(nameof(FieldNameExpression), "Name of the property (can also be nested, like Address.Street)")]
[ParameterRequired(nameof(FieldNameExpression), true)]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression cannot be empty, Fieldname [x] is not found on type [y], Field is not of type [z]")]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the field (property)", "This will be returned if the field (property) is found")]
public partial record TypedFieldExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Expression ToUntyped() => new FieldExpression(Expression, FieldNameExpression);

    public Result<T> EvaluateTyped(object? context)
    {
        var untypedResult = FieldExpression.Evaluate(context, Expression, FieldNameExpression);
        if (untypedResult.IsSuccessful())
        {
            var typedResult = untypedResult.TryCast<T>($"Field is not of type [{typeof(T).FullName}]");

            if (typedResult.IsSuccessful() && typedResult.GetValue() is null)
            {
                //HACK: Null values now work differently with TryCast. We need a new method on Result to fix this... For now, do a work-around.
                return Result.Invalid<T>($"Field is not of type [{typeof(T).FullName}]");
            }

            return typedResult;
        }
        else
        {
            return Result.FromExistingResult<T>(untypedResult);
        }
    }
}
