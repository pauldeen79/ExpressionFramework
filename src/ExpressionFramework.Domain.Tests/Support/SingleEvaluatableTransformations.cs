namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class SingleEvaluatableTransformations
{
    [StepArgumentTransformation]
    public static SingleEvaluatable SingleEvaluatableTransform(Table table)
        => table.CreateInstance<SingleEvaluatableModel>().ToEvaluatable();
}
