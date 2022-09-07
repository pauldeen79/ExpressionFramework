namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private readonly ContextSteps _contextSteps;

    private ExpressionBuilder? _expressionBuilder;
    private Result<object?>? _result;

    private ChainedExpressionBuilder ChainedExpressionBuilder => _expressionBuilder as ChainedExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a chained expression?");
    private Expression Expression => (_expressionBuilder ?? throw new InvalidOperationException("First initialize the expression using 'Given I have a ... expression'")).Build();
    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    public ExpressionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the constant expression '([^']*)'")]
    public void GivenIHaveTheConstantExpression(string expression)
        => _expressionBuilder = new ConstantExpressionBuilder().WithValue(StringExpression.Parse(expression));

    [Given(@"I have the delegate expression '([^']*)'")]
    public void GivenIHaveTheDelegateExpression(string expression)
        => _expressionBuilder = new DelegateExpressionBuilder().WithValue(_ => StringExpression.Parse(expression));

    [Given(@"I have an empty expression")]
    public void GivenIHaveAnEmptyExpression()
        => _expressionBuilder = new EmptyExpressionBuilder();

    [Given(@"I have a context expression")]
    public void GivenIHaveAContextExpression()
        => _expressionBuilder = new ContextExpressionBuilder();

    [Given(@"I have an unknown expression")]
    public void GivenIHaveAnUnknownExpression()
        => _expressionBuilder = new UnknownExpressionBuilder();

    [Given(@"I have a chained expression")]
    public void GivenIHaveAChainedExpression()
        => _expressionBuilder = new ChainedExpressionBuilder();

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

    [Given(@"I chain a constant expression '([^']*)' to it")]
    public void GivenIChainAConstantExpressionToIt(string expression)
        => ChainedExpressionBuilder.AddExpressions(new ConstantExpressionBuilder().WithValue(expression));

    [Given(@"I chain a to upper case expression to it")]
    public void GivenIChainAToUpperCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToUpperCaseExpressionBuilder());
}
