namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates a set of cases, and returns the result of the first valid case")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Context that will be used as context on the condition of each case")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(false)]
[ParameterDescription(nameof(Cases), "Set of cases (scenarios)")]
[ParameterRequired(nameof(Cases), true)]
[ParameterDescription(nameof(DefaultExpression), "Optional expression to use when none of the cases evaluates to false. When left empty, and empty expression will be used.")]
[ParameterRequired(nameof(DefaultExpression), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the ResultExpression of the first valid case, value of the DefaultExpression or empty value", "Value of the ResultExpression of the first valid case when condition evaluates to true, value of the DefaultExpression (when available and all cases evaluate to false), or empty value (when DefaultExpression is not provided and condition evaluates to false")]
[ReturnValue(ResultStatus.Error, "Empty", "Any unsuccessful result from either the case evaluations, result expression evaluation or default expression evaluation")]
public partial record SwitchExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        foreach (var @case in Cases)
        {
            var conditionalExpression = new ConditionalExpression(@case.Condition, @case.Expression, null);
            var caseResult = conditionalExpression.EvaluateWithConditionResult(context);
            if (!caseResult.IsSuccessful())
            {
                return Result<object?>.FromExistingResult(caseResult);
            }
            if (caseResult.Value.ConditionResult)
            {
                return caseResult.Value.ExpressionResult;
            }
        }

        if (DefaultExpression != null)
        {
            return DefaultExpression.Evaluate(context);
        }

        return Result<object?>.Success(null);
    }
}
