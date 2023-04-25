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
[ReturnValue(ResultStatus.Ok, typeof(string), "Expression with replaced value", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string, FindExpression must be of type string, ReplaceExpression must be of type string")]
public partial record StringReplaceExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
    {
        var findExpressionResult = FindExpression.EvaluateTypedWithTypeCheck(context, "FindExpression is not of type string");
        if (!findExpressionResult.IsSuccessful())
        {
            return findExpressionResult;
        }

        var replaceExpressionResult = ReplaceExpression.EvaluateTypedWithTypeCheck(context, "ReplaceExpression is not of type string");
        if (!replaceExpressionResult.IsSuccessful())
        {
            return replaceExpressionResult;
        }

        return Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? Result<string>.Success(result.Value!.Replace(findExpressionResult.Value!, replaceExpressionResult.Value!))
                : Result<string>.FromExistingResult(result));
    }
}
