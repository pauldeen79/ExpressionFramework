namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class ConditionTransformations
{
    [StepArgumentTransformation]
    public static Condition DialogPartResultAnswerTransform(Table table)
        => table.CreateInstance<ConditionModel>().ToCondition();
}
