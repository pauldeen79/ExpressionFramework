namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private Expression? _expression;
    private object? _context;
    private Result<object?>? _result;

    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    [Given(@"I have the expression '([^']*)'")]
    public void GivenIHaveTheExpression(string expression)
        => _expression = new ConstantExpression(ValueExpression.Evaluate(expression));

    [Given(@"I have an unknown expression")]
    public void GivenIHaveAnUnknownExpression()
        => _expression = new UnknownExpression();

    [When(@"I evaluate the expression")]
    public async Task WhenIEvaluateTheExpression()
        => _result = await ApplicationEntrypoint.ExpressionEvaluator.Evaluate(_context, _expression ?? throw new InvalidOperationException("First initialize the expression using 'Given I have the expression'"));

    [Then(@"the expression result should be '([^']*)'")]
    public void ThenTheExpressionResultShouldBe(string expectedResult)
        => Result.Value.Should().BeEquivalentTo(expectedResult);

    [Then(@"the expression result should be unsuccessful")]
    public void ThenTheExpressionResultShouldBeUnsuccessful()
        => Result.Status.Should().Be(ResultStatus.Invalid);
}
