﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Trims the start characters of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to trim start end characters of")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(TrimChars), "Optional trim characters to use. When empty, space will be used")]
[ParameterRequired(nameof(TrimChars), false)]
[ReturnValue(ResultStatus.Ok, "The trim start value of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record TrimStartExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(TrimStart(s))
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);

    public TrimStartExpression() : this(default(IEnumerable<char>)) { }

    private string TrimStart(string s) => TrimChars == null ? s.TrimStart() : s.TrimStart(TrimChars.ToArray());
}
