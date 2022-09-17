namespace ExpressionFramework.Domain.Tests.StepDefinitions;

[Binding]
public sealed class EvaluatableSteps
{
    private readonly ContextSteps _contextSteps;
    
    private Result<bool>? _result;
    private readonly ComposedEvaluatableBuilder _composedEvaluatableBuilder = new ComposedEvaluatableBuilder();

    private Result<bool> Result => _result ?? throw new InvalidOperationException("First evaluate the condition using 'When I evaluate the condition'");
    public EvaluatableBuilder EvaluatableBuilder => _composedEvaluatableBuilder;

    public EvaluatableSteps(ContextSteps contextSteps) => _contextSteps = contextSteps;

    [Given(@"I have the following condition")]
    public void GivenIHaveTheFollowingCondition(SingleEvaluatable condition)
        => _composedEvaluatableBuilder.AddConditions(new SingleEvaluatableBuilder(condition));

    [Given(@"I have the following conditions")]
    public void GivenIHaveTheFollowingCondition(SingleEvaluatable[] conditions)
        => _composedEvaluatableBuilder.AddConditions(conditions.Select(x => new SingleEvaluatableBuilder(x)));

    [When(@"I evaluate the condition")]
    public void WhenIEvaluateTheCondition()
        => _result = _composedEvaluatableBuilder.Build().Evaluate(_contextSteps.Context);

    [Then(@"the condition evaluation result should contain the content")]
    public void ValidateResponseContent(Table table)
        => table.CompareToInstance(Result);
}
