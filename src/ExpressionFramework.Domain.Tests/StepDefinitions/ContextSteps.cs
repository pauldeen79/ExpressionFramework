﻿namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ContextSteps
{
    private object? _context;

    [Given(@"I set the context to '([^']*)'")]
    public void GivenISetTheContextTo(string context)
        => _context = StringExpression.Evaluate(context);

    public object? Context => _context;
}
