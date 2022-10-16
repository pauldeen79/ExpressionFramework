﻿namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICompoundExpression : IExpression
{
    [Required]
    IExpression SecondExpression { get; }
    [Required]
    IAggregator Aggregator { get; }
}
