namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the position of the find expression within the (string) expression")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to find text in")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ParameterDescription(nameof(FindExpression), "String to find")]
[ParameterRequired(nameof(FindExpression), true)]
[ParameterType(nameof(FindExpression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(int), "The (zero-based) index of the text, or -1 when not found", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string, FindExpression must be of type string")]
public partial record StringFindExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));
    
    public Result<int> EvaluateTyped(object? context)
    {
        var findExpressionResult = FindExpression.EvaluateTypedWithTypeCheck(context, "FindExpression is not of type string");
        if (!findExpressionResult.IsSuccessful())
        {
            return Result.FromExistingResult<int>(findExpressionResult);
        }

        return Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? Result.Success(result.Value!.IndexOf(findExpressionResult.Value!))
                : Result.FromExistingResult<int>(result));
    }
}
