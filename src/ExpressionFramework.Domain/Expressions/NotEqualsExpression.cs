namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(NotEqualsExpression))]
public partial record NotEqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result<object?>.Success(!EqualsOperator.IsValid(results[0], results[1]));
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result<bool>.FromExistingResult(nonSuccessfulResult)
            : Result<bool>.Success(!EqualsOperator.IsValid(results[0], results[1]));
    }

    public Expression ToUntyped() => this;

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor(
            typeof(NotEqualsExpression),
            "Evaluates two expressions, and compares the two results. It will return false when they are equal, or true otherwise.",
            "true of false",
            "This result will always be returned",
            null,
            "Boolean expression to perform NotEquals operation on");

    public NotEqualsExpression(object? firstExpression, object? secondExpression) : this(new ConstantExpression(firstExpression), new ConstantExpression(secondExpression)) { }
    public NotEqualsExpression(Func<object?, object?> firstExpression, Func<object?, object?> secondExpression) : this(new DelegateExpression(firstExpression), new DelegateExpression(secondExpression)) { }
}

public partial record NotEqualsExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
