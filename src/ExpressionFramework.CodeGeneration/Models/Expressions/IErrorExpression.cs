﻿namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IErrorExpression : IExpression
{
    [Required]
    string ErrorMessage { get; }
}
