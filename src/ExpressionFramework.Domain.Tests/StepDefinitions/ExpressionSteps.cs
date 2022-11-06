namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private readonly ContextSteps _contextSteps;
    private readonly EvaluatableSteps _evaluatableSteps;

    private ExpressionBuilder? _expressionBuilder;
    private Result<object?>? _result;

    private ChainedExpressionBuilder ChainedExpressionBuilder => _expressionBuilder as ChainedExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a chained expression?");
    private IfExpressionBuilder IfExpressionBuilder => _expressionBuilder as IfExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized an if expression?");
    private SwitchExpressionBuilder SwitchExpressionBuilder => _expressionBuilder as SwitchExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a switch expression?");
    private Expression Expression => _expressionBuilder != null
        ? _expressionBuilder.Build()
        : throw new InvalidOperationException("First initialize the expression using 'Given I have a ... expression'");

    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    public ExpressionSteps(ContextSteps contextSteps, EvaluatableSteps conditionSteps)
    {
        _contextSteps = contextSteps;
        _evaluatableSteps = conditionSteps;
    }

    [Given(@"I have the constant expression '([^']*)'")]
    public void GivenIHaveTheConstantExpression(string expression)
        => _expressionBuilder = new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(expression));

    [Given(@"I have the delegate expression '([^']*)'")]
    public void GivenIHaveTheDelegateExpression(string expression)
        => _expressionBuilder = new DelegateExpressionBuilder().WithValue(_ => StringExpressionParser.Parse(expression));

    [Given(@"I have an empty expression")]
    public void GivenIHaveAnEmptyExpression()
        => _expressionBuilder = new EmptyExpressionBuilder();

    [Given(@"I have a context expression")]
    public void GivenIHaveAContextExpression()
        => _expressionBuilder = new ContextExpressionBuilder();

    [Given(@"I have a chained expression")]
    public void GivenIHaveAChainedExpression()
        => _expressionBuilder = new ChainedExpressionBuilder();

    [Given(@"I have a conditional expression")]
    public void GivenIHaveAConditionalExpression()
        => _expressionBuilder = new IfExpressionBuilder().WithCondition(_evaluatableSteps.EvaluatableBuilder);

    [Given(@"I have a switch expression")]
    public void GivenIHaveASwitchExpression()
        => _expressionBuilder = new SwitchExpressionBuilder();

    [Given(@"I have the equals expression with first value '([^']*)' and second value '([^']*)'")]
    public void GivenIHaveTheEqualsExpressionWithFirstValueAndSecondValue(string firstValue, string secondValue)
        => _expressionBuilder = new EqualsExpressionBuilder()
        .WithFirstExpression(new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(firstValue)))
        .WithSecondExpression(new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(secondValue)));

    [Given(@"I chain a constant expression '([^']*)' to it")]
    public void GivenIChainAConstantExpressionToIt(string expression)
        => ChainedExpressionBuilder.AddExpressions(new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(expression)));

    [Given(@"I chain a to upper case expression to it")]
    public void GivenIChainAToUpperCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToUpperCaseExpressionBuilder().WithExpression(new ContextExpressionBuilder()));

    [Given(@"I chain a to lower case expression to it")]
    public void GivenIChainAToLowerCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToLowerCaseExpressionBuilder().WithExpression(new ContextExpressionBuilder()));

    [Given(@"I chain a to pascal case expression to it")]
    public void GivenIChainAToPascalCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToPascalCaseExpressionBuilder().WithExpression(new ContextExpressionBuilder()));

    [Given(@"I use a constant expression '([^']*)' as result expression")]
    public void GivenIUseAConstantExpressionAsResultExpression(string expression)
        => IfExpressionBuilder.ResultExpression = new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(expression));

    [Given(@"I use a constant expression '([^']*)' as default expression")]
    public void GivenIUseAConstantExpressionAsDefaultExpression(string expression)
        => IfExpressionBuilder.DefaultExpression = new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(expression));

    [Given(@"I add the following case")]
    public void GivenIAddTheFollowingCase(Case @case)
        => SwitchExpressionBuilder.AddCases(new CaseBuilder(@case));

    [Given(@"I set the default expression to '([^']*)'")]
    public void GivenISetTheDefaultExpressionTo(string expression)
        => SwitchExpressionBuilder.DefaultExpression = new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(expression));

    [When(@"I evaluate the expression")]
    public void WhenIEvaluateTheExpression()
        => _result = Expression.Evaluate(_contextSteps.Context);

    [Then(@"the expression evaluation result should contain the content")]
    public void ValidateResponseContent(Table table)
        => table.CompareToInstance(Result);

    [Then(@"the expression evaluation result value should be empty")]
    public void ThenTheExpressionEvaluationResultValueShouldBeEmpty()
        => Result.Value.Should().BeNull();

    [Then(@"the expression evaluation result value should be '([^']*)'")]
    public void ThenTheExpressionEvaluationResultValueShouldBe(string value)
        => Result.Value.Should().BeEquivalentTo(StringExpressionParser.Parse(value));
}
