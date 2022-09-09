namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class ConditionTransformations
{
    [StepArgumentTransformation]
    public static Condition ConditionTransform(Table table)
        => table.CreateInstance<ConditionModel>().ToCondition();
}
