namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class ConditionSteps
{
    private readonly ContextSteps _contextSteps;
    
    private Result<bool>? _result;
    private SingleEvaluatable[]? _conditions;

    private Result<bool> Result => _result ?? throw new InvalidOperationException("First evaluate the condition using 'When I evaluate the condition'");
    public SingleEvaluatable[] Conditions => _conditions ?? Array.Empty<SingleEvaluatable>();

    public ConditionSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the following condition")]
    public void GivenIHaveTheFollowingCondition(SingleEvaluatable condition)
        => _conditions = new[] { condition };

    [Given(@"I have the following conditions")]
    public void GivenIHaveTheFollowingCondition(SingleEvaluatable[] conditions)
        => _conditions = conditions;

    [When(@"I evaluate the condition")]
    public void WhenIEvaluateTheCondition()
        => _result = new ComposedEvaluatable(Conditions).Evaluate(_contextSteps.Context);

    [Then(@"the condition evaluation result should contain the content")]
    public void ValidateResponseContent(Table table)
        => table.CompareToInstance(Result);
}
