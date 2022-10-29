namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(OrExpression))]
public partial record OrExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a || b), x => x);

    public Result<bool> EvaluateTyped(object? context)
        => BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a || b);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor
        (
            typeof(OrExpression),
            "Returns the OR-combination value of the boolean context value and the expression",
            "OR-combinated value of the boolean context value and the expression",
            "This result will be returned when the context and the expression are boolean values",
            "Context must be of type boolean, Expression must be of type boolean",
            "Boolean expression to perform OR combination on"
        );

}

