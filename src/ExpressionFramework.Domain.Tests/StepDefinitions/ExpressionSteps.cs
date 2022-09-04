namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private Expression? _expression;
    private object? _context;
    private Result<object?>? _result;

    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    [Given(@"I have the constant expression '([^']*)'")]
    public void GivenIHaveTheConstantExpression(string expression)
        => _expression = new ConstantExpression(StringExpression.Evaluate(expression));

    [Given(@"I have an empty expression")]
    public void GivenIHaveAnEmptyExpression()
        => _expression = new EmptyExpression();

    [Given(@"I have a context expression")]
    public void GivenIHaveAContextExpression()
        => _expression = new ContextExpression();

    [Given(@"I have an unknown expression")]
    public void GivenIHaveAnUnknownExpression()
        => _expression = new UnknownExpression();

    [Given(@"I set the context to '([^']*)'")]
    public void GivenISetTheContextTo(string context)
        => _context = StringExpression.Evaluate(context);

    [When(@"I evaluate the expression")]
    public async Task WhenIEvaluateTheExpression()
        => _result = await ApplicationEntrypoint.ExpressionEvaluator.Evaluate(_context, _expression ?? throw new InvalidOperationException("First initialize the expression using 'Given I have the expression'"));

    [Then(@"the result value should be '([^']*)'")]
    public void ThenTheResultValueShouldBe(string expectedResult)
        => Result.Value.Should().BeEquivalentTo(StringExpression.Evaluate(expectedResult));

    [Then(@"the result status should be '([^']*)'")]
    public void ThenTheResultStatusShouldBe(ResultStatus status)
        => Result.Status.Should().Be(status);
}
