﻿namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    [Required]
    ITypedExpression<string> ErrorMessageExpression { get; }
    IMultipleTypedExpressions<ValidationError>? ValidationErrorExpressions { get; }
}
