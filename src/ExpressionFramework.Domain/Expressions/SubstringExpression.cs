﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters from the specified position of a string value of the context")]
[UsesContext(true)]
[ContextDescription("String to get a number of characters for")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ParameterDescription(nameof(IndexExpression), "Zero-based start position of the characters to return")]
[ParameterRequired(nameof(IndexExpression), true)]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(string), "A set of characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string, IndexExpression did not return an integer, LengthExpression did not return an integer, Index and length must refer to a location within the string")]
public partial record SubstringExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => EvaluateTyped(context).TryCast<object?>();

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<string>.Invalid("Context must be of type string");

    private Result<string> GetLeftValueFromString(string s)
    {
        var indexResult = IndexExpression.Evaluate(s);
        if (!indexResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(indexResult);
        }

        if (indexResult.Value is not int index)
        {
            return Result<string>.Invalid("IndexExpression did not return an integer");
        }

        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        if (lengthResult.Value is not int length)
        {
            return Result<string>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= index + length
            ? Result<string>.Success(s.Substring(index, length))
            : Result<string>.Invalid("Index and length must refer to a location within the string");
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context, () => PerformAdditionalValidation(context));

    private IEnumerable<ValidationResult> PerformAdditionalValidation(object? context)
    {
        if (context is not string s)
        {
            yield break;
        }

        int? localIndex = null;
        int? localLength = null;

        var indexResult = IndexExpression.Evaluate(s);
        if (indexResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"IndexExpression returned an invalid result. Error message: {indexResult.ErrorMessage}");
        }
        else if (indexResult.Status == ResultStatus.Ok)
        {
            if (indexResult.Value is not int index)
            {
                yield return new ValidationResult($"IndexExpression did not return an integer");
            }
            else
            {
                localIndex = index;
            }
        }

        var lengthResult = LengthExpression.Evaluate(s);
        if (lengthResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"LengthExpression returned an invalid result. Error message: {lengthResult.ErrorMessage}");
        }
        else if (lengthResult.Status == ResultStatus.Ok)
        {
            if (lengthResult.Value is not int length)
            {
                yield return new ValidationResult($"LengthExpression did not return an integer");
            }
            else
            {
                localLength = length;
            }
        }

        if (localIndex.HasValue && localLength.HasValue && s.Length < localIndex + localLength)
        {
            yield return new ValidationResult("Index and length must refer to a location within the string");
        }
    }

    public SubstringExpression(int index, int length) : this(new ConstantExpression(index), new ConstantExpression(length)) { }
}

