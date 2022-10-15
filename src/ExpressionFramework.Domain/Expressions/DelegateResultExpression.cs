namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a result from a delegate")]
[ExpressionUsesContext(false)]
[ParameterDescription(nameof(Result), "Delegate to use")]
[ParameterRequired(nameof(Result), true)]
[ParameterType(nameof(Result), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(object), "The result from the delegate that is supplied with the Value parameter", "This result will always be returned. (note that the status can be anything)")]
public partial record DelegateResultExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.Invoke(context);
}

