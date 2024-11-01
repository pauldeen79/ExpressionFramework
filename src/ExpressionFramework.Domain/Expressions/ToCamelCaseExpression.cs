namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to camel case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the camel case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to camel case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToCamelCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => StringExpression.EvaluateCultureExpression(Expression, Culture, context, (culture, value) => value.ToCamelCase(culture), value => value.ToCamelCase(CultureInfo.CurrentCulture));
}
