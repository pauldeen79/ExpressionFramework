namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(NotEqualsExpression))]
public partial record NotEqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = Array.Find(results, x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result.FromExistingResult<object?>(NotEqual.Evaluate(results[0], results[1], StringComparison.CurrentCultureIgnoreCase));
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = Array.Find(results, x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result.FromExistingResult<bool>(nonSuccessfulResult)
            : NotEqual.Evaluate(results[0], results[1], StringComparison.CurrentCultureIgnoreCase);
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor(
            typeof(NotEqualsExpression),
            "Evaluates two expressions, and compares the two results. It will return false when they are equal, or true otherwise.",
            "true of false",
            "This result will always be returned",
            null,
            "Boolean expression to perform NotEquals operation on");
}
