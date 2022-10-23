namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the OR-combination value of the boolean context value and the expression")]
[UsesContext(true)]
[ContextDescription("Boolean value to invert")]
[ContextType(typeof(bool))]
[ContextRequired(true)]
[ParameterDescription(nameof(Expression), "Boolean expression to perform OR combination on")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(bool))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "OR-combinated value of the boolean context value and the expression", "This result will be returned when the context and the expression are boolean values")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type boolean, Expression must be of type boolean")]
public partial record OrExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (context is not bool b)
        {
            return Result<object?>.Invalid("Context must be of type boolean");
        }

        var expressionResult = Expression.EvaluateTyped<bool>(context, "Expression must be of type boolean");
        if (!expressionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(expressionResult);
        }

        return Result<object?>.Success(b || expressionResult.Value);
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        if (context is not bool b)
        {
            return Result<bool>.Invalid("Context must be of type boolean");
        }

        var expressionResult = Expression.EvaluateTyped<bool>(context, "Expression must be of type boolean");
        if (!expressionResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(expressionResult);
        }

        return Result<bool>.Success(b || expressionResult.Value);
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not bool)
        {
            yield return new ValidationResult("Context must be of type boolean");
        }
    }
}

