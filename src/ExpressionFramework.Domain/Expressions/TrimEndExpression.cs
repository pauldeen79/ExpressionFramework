﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Trims the end characters of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to trim the end characters of")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(TrimChars), "Optional trim characters to use. When empty, space will be used")]
[ParameterRequired(nameof(TrimChars), false)]
[ReturnValue(ResultStatus.Ok, "The trim end value of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record TrimEndExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(TrimEnd(s))
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpressionBase.ValidateContext(context);

    public TrimEndExpression() : this(default(IEnumerable<char>)) { }

    private string TrimEnd(string s) => TrimChars == null ? s.TrimEnd() : s.TrimEnd(TrimChars.ToArray());
}

