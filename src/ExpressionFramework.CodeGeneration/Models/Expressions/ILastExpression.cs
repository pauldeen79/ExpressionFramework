﻿namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastExpression : IExpression
{
    IExpression? PredicateExpression { get; }
}
