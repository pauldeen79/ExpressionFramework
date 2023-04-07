namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(OrExpression))]
public partial record OrExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<bool> EvaluateTyped(object? context)
        => BooleanExpression.EvaluateBooleanCombination(context, FirstExpression, SecondExpression, (a, b) => a || b);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor
        (
            typeof(OrExpression),
            "Returns the OR-combination value of two boolean expressions",
            "OR-combinated value of the two boolean expressions",
            "This result will be returned when both expressions are boolean values",
            "FirstExpression must be of type boolean, SecondExpression must be of type boolean",
            "Boolean expression to perform OR combination on"
        );
}

public partial record OrExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
