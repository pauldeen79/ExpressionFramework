namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates two expressions, and compares the two results. It will return true when they are equal, or false otherwise.")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Context to use on expression evaluation")]
[ExpressionContextRequired(false)]
[ExpressionContextType(typeof(object))]
[ParameterDescription(nameof(FirstExpression), "First expression")]
[ParameterRequired(nameof(FirstExpression), true)]
[ParameterType(nameof(FirstExpression), typeof(object))]
[ParameterDescription(nameof(SecondExpression), "Second expression")]
[ParameterRequired(nameof(SecondExpression), true)]
[ParameterType(nameof(SecondExpression), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true of false", "This result will always be returned")]
public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result<object?>.Success(EqualsOperator.IsValid(results[0], results[1]));
    }
}
