namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ConditionSteps
{
    private readonly ContextSteps _contextSteps;
    
    private Result<bool>? _result;
    private Condition? _condition;

    private Result<bool> Result => _result ?? throw new InvalidOperationException("First evaluate the condition using 'When I evaluate the condition'");
    private Condition Condition => _condition ?? throw new InvalidOperationException("First initialize the condition using 'Given I have the condition'");

    public ConditionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the following condition")]
    public void GivenIHaveTheFollowingCondition(Condition condition)
        => _condition = condition;

    [When(@"I evaluate the condition")]
    public async Task WhenIEvaluateTheCondition()
        => _result = await ApplicationEntrypoint.ConditionEvaluator.Evaluate(_contextSteps.Context, new[] { Condition });

    [Then(@"the condition evaluation result value should be '([^']*)'")]
    public void ThenTheConditionEvaluationResultValueShouldBe(string result)
        => Result.Value.Should().Be(result.IsTrue());

    [Then(@"the condition evaluation result status should be '([^']*)'")]
    public void ThenTheConditionEvaluationResultStatusShouldBe(ResultStatus status)
        => Result.Status.Should().Be(status);
}
