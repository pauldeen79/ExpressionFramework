namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AndExpression))]
public partial record AndExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

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

    public AndExpression(object? firstExpression, object? secondExpression) : this(new ConstantExpression(firstExpression), new ConstantExpression(secondExpression)) { }
    public AndExpression(Func<object?, object?> firstExpression, Func<object?, object?> secondExpression) : this(new DelegateExpression(firstExpression), new DelegateExpression(secondExpression)) { }
}

public partial record AndExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
