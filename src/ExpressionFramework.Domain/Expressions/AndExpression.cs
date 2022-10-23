namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AndExpression))]
public partial record AndExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a && b), x => x);

    public Result<bool> EvaluateTyped(object? context)
        => BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a && b);

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not bool)
        {
            yield return new ValidationResult("Context must be of type boolean");
        }
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor
        (
            typeof(AndExpression),
            "Returns the AND-combination value of the boolean context value and the expression",
            "AND-combinated value of the boolean context value and the expression",
            "This result will be returned when the context and the expression are boolean values",
            "Context must be of type boolean, Expression must be of type boolean",
            "Boolean expression to perform AND combination on"
        );
}

