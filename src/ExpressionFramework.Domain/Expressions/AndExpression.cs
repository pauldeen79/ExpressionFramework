﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the AND-combination value of the boolean context value and the expression")]
[UsesContext(true)]
[ContextDescription("Boolean value to invert")]
[ContextType(typeof(bool))]
[ContextRequired(true)]
[ParameterDescription(nameof(Expression), "Boolean expression to perform AND combination on")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(bool))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "AND-combinated value of the boolean context value and the expression", "This result will be returned when the context and the expression are boolean values")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type boolean, Expression must be of type boolean")]
public partial record AndExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a && b), x => x);

    public Result<bool> EvaluateTyped(object? context)
        => BooleanExpression.EvaluateBooleanCombination(context, Expression, (a, b) => a && b);

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not bool)
        {
            yield return new ValidationResult("Context must be of type boolean");
        }
    }
}

