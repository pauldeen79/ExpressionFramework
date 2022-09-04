namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private readonly ContextSteps _contextSteps;

    private Expression? _expression;
    private Result<object?>? _result;

    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    public ExpressionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

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

    [When(@"I evaluate the expression")]
    public async Task WhenIEvaluateTheExpression()
        => _result = await ApplicationEntrypoint.ExpressionEvaluator.Evaluate(_contextSteps.Context, _expression ?? throw new InvalidOperationException("First initialize the expression using 'Given I have the expression'"));

    [Then(@"the expression result value should be '([^']*)'")]
    public void ThenTheExpressionResultValueShouldBe(string expectedResult)
        => Result.Value.Should().BeEquivalentTo(StringExpression.Evaluate(expectedResult));

    [Then(@"the expression result status should be '([^']*)'")]
    public void ThenTheExpressionResultStatusShouldBe(ResultStatus status)
        => Result.Status.Should().Be(status);
}
