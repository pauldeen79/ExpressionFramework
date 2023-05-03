namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AndExpression))]
public partial record AndExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public Result<bool> EvaluateTyped(object? context)
        => BooleanExpression.EvaluateBooleanCombination(context, FirstExpression, SecondExpression, (a, b) => a && b);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor
        (
            typeof(AndExpression),
            "Returns the AND-combination value of two boolean expressions",
            "AND-combinated value of the two boolean expressions",
            "This result will be returned when both expressions are boolean values",
            "FirstExpression must be of type boolean, SecondExpression must be of type boolean",
            "Boolean expression to perform AND combination on"
        );
}
