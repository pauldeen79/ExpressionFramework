namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to pascal case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the pascal case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to pascal case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToPascalCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => StringExpression.EvaluateCultureExpression(Expression, Culture, context, (culture, value) => value.ToPascalCase(culture), value => value.ToPascalCase(CultureInfo.CurrentCulture));
}
