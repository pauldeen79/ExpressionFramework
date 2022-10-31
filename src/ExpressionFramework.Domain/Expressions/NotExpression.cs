namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the inverted value of the boolean value")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "Boolean to invert")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(bool))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "Inverted value of the boolean context value", "This result will be returned when the expression is a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type boolean")]
public partial record NotExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
    {
        var boolResult = Expression.EvaluateTyped<bool>(context, "Expression must be of type boolean");
        if (!boolResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(boolResult);
        }

        return Result<object?>.Success(!boolResult.Value!);
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var boolResult = Expression.EvaluateTyped<bool>(context, "Expression must be of type boolean");
        if (!boolResult.IsSuccessful())
        {
            return boolResult;
        }

        return Result<bool>.Success(!boolResult.Value!);
    }
}

