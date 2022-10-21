﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the end of a string value of the context")]
[UsesContext(true)]
[ContextDescription("String to get the last characters for")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The last characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record RightExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? GetRightValueFromString(s)
            : Result<string>.Invalid("Context must be of type string");

    private Result<string> GetRightValueFromString(string s)
    {
        var lengthResult = LengthExpression.EvaluateTyped<int>(s, "LengthExpression did not return an integer");
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        return s.Length >= lengthResult.Value
            ? Result<string>.Success(s.Substring(s.Length - lengthResult.Value, lengthResult.Value))
            : Result<string>.Invalid("Length must refer to a location within the string");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context, () => StringExpression.ValidateLength(context, LengthExpression));

    public RightExpression(int length) : this(new ConstantExpression(length)) { }
}
