namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the length of the (string) context")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the length for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(int), "The length of the (string) expression", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record StringLengthExpression : ITypedExpression<int>
{
    public override Result<object?> Evaluate(object? context)
    {
        var stringResult = Expression.EvaluateTyped<string>(context, "Expression must be of type string");
        if (!stringResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(stringResult);
        }

        return Result<object?>.Success(stringResult.Value!.Length);
    }

    public Result<int> EvaluateTyped(object? context)
    {
        var stringResult = Expression.EvaluateTyped<string>(context, "Expression must be of type string");
        if (!stringResult.IsSuccessful())
        {
            return Result<int>.FromExistingResult(stringResult);
        }

        return Result<int>.Success(stringResult.Value!.Length);
    }
}

