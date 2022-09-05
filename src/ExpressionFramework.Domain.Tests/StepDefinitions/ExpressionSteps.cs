namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private readonly ContextSteps _contextSteps;

    private Expression? _expression;
    private Result<object?>? _result;

    private Expression Expression => _expression ?? throw new InvalidOperationException("First initialize the expression using 'Given I have a ... expression'");
    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    public ExpressionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the constant expression '([^']*)'")]
    public void GivenIHaveTheConstantExpression(string expression)
        => _expression = new ConstantExpression(StringExpression.Parse(expression));

    [Given(@"I have the delegate expression '([^']*)'")]
    public void GivenIHaveTheDelegateExpression(string expression)
        => _expression = new DelegateExpression(_ => StringExpression.Parse(expression));

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
        => _result = await ApplicationEntrypoint.ExpressionEvaluator.Evaluate(_contextSteps.Context, Expression);

    [Then(@"the expression evaluation result should contain the content")]
    public void ValidateResponseContent(Table table)
        => table.CompareToInstance(Result);

    [Then(@"the expression evaluation result value should be empty")]
    public void ThenTheExpressionEvaluationResultValueShouldBeEmpty()
        => Result.Value.Should().BeNull();

    [Then(@"the expression evaluation result value should be '([^']*)'")]
    public void ThenTheExpressionEvaluationResultValueShouldBe(string value)
        => Result.Value.Should().BeEquivalentTo(StringExpression.Parse(value));
}
