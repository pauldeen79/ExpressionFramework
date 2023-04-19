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
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);
    
    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression);

    public Result<int> EvaluateTyped(object? context)
    {
        var findExpressionResult = FindExpression.EvaluateTyped<string>(context, "FindExpression must be of type string");
        if (!findExpressionResult.IsSuccessful())
        {
            return Result<int>.FromExistingResult(findExpressionResult);
        }
        return Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? Result<int>.Success(result.Value!.IndexOf(findExpressionResult.Value!))
                : Result<int>.FromExistingResult(result));
    }

    public StringFindExpression(object? expression, object? findExpression) : this(new ConstantExpression(expression), new ConstantExpression(findExpression)) { }
    public StringFindExpression(Func<object?, object?> expression, Func<object?, object?> findExpression) : this(new DelegateExpression(expression), new DelegateExpression(findExpression)) { }
}

public partial record StringFindExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
