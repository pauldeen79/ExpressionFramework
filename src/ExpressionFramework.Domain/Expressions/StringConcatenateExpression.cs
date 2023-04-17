namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Concatenates strings")]
[UsesContext(true)]
[ContextDescription("Value to use as context in expression evaluation")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Expressions), "Strings to concatenate")]
[ParameterType(nameof(Expressions), typeof(string))]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(string), "The concatenated string", "This result will be returned when the expressions are all of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "At least one expression is required, Expressions must be of type string")]
public partial record StringConcatenateExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
    {
        var values = Expressions.EvaluateTypedUntilFirstError<string>(context, "Expressions must be of type string");
        if (!values.Any())
        {
            return Result<string>.Invalid("At least one expression is required");
        }

        if (!values.Last().IsSuccessful())
        {
            return Result<string>.FromExistingResult(values.Last());
        }

        return Result<string>.Success(string.Concat(values.Select(x => x.Value)));
    }

    public Expression ToUntyped() => this;

    public StringConcatenateExpression(IEnumerable<object?> expressions) : this(expressions.Select(x => new ConstantExpression(x))) { }
    public StringConcatenateExpression(IEnumerable<Func<object?, object?>> expressions) : this(expressions.Select(x => new DelegateExpression(x))) { }
}

public partial record StringConcatenateExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
