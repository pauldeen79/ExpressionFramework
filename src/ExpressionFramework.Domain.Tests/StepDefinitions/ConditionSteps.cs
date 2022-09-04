namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ConditionSteps
{
    private readonly ContextSteps _contextSteps;
    
    private Result<bool>? _result;
    private Condition[]? _conditions;

    private Result<bool> Result => _result ?? throw new InvalidOperationException("First evaluate the condition using 'When I evaluate the condition'");
    private Condition[] Conditions => _conditions ?? throw new InvalidOperationException("First initialize the condition using 'Given I have the condition'");

    public ConditionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the following condition")]
    public void GivenIHaveTheFollowingCondition(Condition condition)
        => _conditions = new[] { condition };

    [When(@"I evaluate the condition")]
    public async Task WhenIEvaluateTheCondition()
        => _result = await ApplicationEntrypoint.ConditionEvaluator.Evaluate(_contextSteps.Context, Conditions);

    [Then(@"the condition evaluation result should contain the content")]
    public void ValidateResponseContent(Table table)
        => table.CompareToInstance(Result);
}
