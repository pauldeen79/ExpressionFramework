﻿namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression PredicateExpression { get; }
}
