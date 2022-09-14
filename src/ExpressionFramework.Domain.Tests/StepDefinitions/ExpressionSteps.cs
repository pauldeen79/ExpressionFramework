namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ExpressionSteps
{
    private readonly ContextSteps _contextSteps;
    private readonly ConditionSteps _conditionSteps;

    private ExpressionBuilder? _expressionBuilder;
    private Result<object?>? _result;

    private ChainedExpressionBuilder ChainedExpressionBuilder => _expressionBuilder as ChainedExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a chained expression?");
    private ConditionalExpressionBuilder ConditionalExpressionBuilder => _expressionBuilder as ConditionalExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a conditional expression?");
    private SwitchExpressionBuilder SwitchExpressionBuilder => _expressionBuilder as SwitchExpressionBuilder ?? throw new InvalidOperationException("Are you sure you initialized a switch expression?");
    private Expression Expression
    {
        get
        {
            if (_expressionBuilder == null)
            {
                throw new InvalidOperationException("First initialize the expression using 'Given I have a ... expression'");
            }

            // Note that currently, we have to add the conditions ourselves, because collection properties on builders are not lazy...
            // To change that, we have to improve ModelFramework.
            var conditionalExpressionBuilder = _expressionBuilder as ConditionalExpressionBuilder;
            if (conditionalExpressionBuilder != null)
            {
                conditionalExpressionBuilder.AddConditions(_conditionSteps.Conditions.Select(x => new ConditionBuilder(x)));
            }

            return _expressionBuilder.Build();
        }
    }

    private Result<object?> Result => _result ?? throw new InvalidOperationException("First evaluate the expression using 'When I evaluate the expression'");

    public ExpressionSteps(ContextSteps contextSteps, ConditionSteps conditionSteps)
    {
        _contextSteps = contextSteps;
        _conditionSteps = conditionSteps;
    }

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

    [Given(@"I have a chained expression")]
    public void GivenIHaveAChainedExpression()
        => _expressionBuilder = new ChainedExpressionBuilder();

    [Given(@"I have a conditional expression")]
    public void GivenIHaveAConditionalExpression()
        => _expressionBuilder = new ConditionalExpressionBuilder();

    [Given(@"I have a switch expression")]
    public void GivenIHaveASwitchExpression()
        => _expressionBuilder = new SwitchExpressionBuilder();

    [Given(@"I have the equals expression with first value '([^']*)' and second value '([^']*)'")]
    public void GivenIHaveTheEqualsExpressionWithFirstValueAndSecondValue(string firstValue, string secondValue)
        => _expressionBuilder = new EqualsExpressionBuilder()
        .WithFirstExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(firstValue)))
        .WithSecondExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(secondValue)));

    [Given(@"I chain a constant expression '([^']*)' to it")]
    public void GivenIChainAConstantExpressionToIt(string expression)
        => ChainedExpressionBuilder.AddExpressions(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(expression)));

    [Given(@"I chain a to upper case expression to it")]
    public void GivenIChainAToUpperCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToUpperCaseExpressionBuilder());

    [Given(@"I chain a to lower case expression to it")]
    public void GivenIChainAToLowerCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToLowerCaseExpressionBuilder());

    [Given(@"I chain a to pascal case expression to it")]
    public void GivenIChainAToPascalCaseExpressionToIt()
        => ChainedExpressionBuilder.AddExpressions(new ToPascalCaseExpressionBuilder());

    [Given(@"I use a constant expression '([^']*)' as result expression")]
    public void GivenIUseAConstantExpressionAsResultExpression(string expression)
        => ConditionalExpressionBuilder.ResultExpression = new ConstantExpressionBuilder().WithValue(StringExpression.Parse(expression));

    [Given(@"I use a constant expression '([^']*)' as default expression")]
    public void GivenIUseAConstantExpressionAsDefaultExpression(string expression)
        => ConditionalExpressionBuilder.DefaultExpression = new ConstantExpressionBuilder().WithValue(StringExpression.Parse(expression));

    [Given(@"I add the following case")]
    public void GivenIAddTheFollowingCase(Case @case)
        => SwitchExpressionBuilder.AddCases(new CaseBuilder(@case));

    [Given(@"I set the default expression to '([^']*)'")]
    public void GivenISetTheDefaultExpressionTo(string expression)
        => SwitchExpressionBuilder.DefaultExpression = new ConstantExpressionBuilder().WithValue(StringExpression.Parse(expression));

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
        => Result.Value.Should().BeEquivalentTo(StringExpression.Parse(value));
}
