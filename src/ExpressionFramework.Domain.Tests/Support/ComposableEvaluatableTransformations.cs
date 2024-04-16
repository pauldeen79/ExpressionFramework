namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class ComposableEvaluatableTransformations
{
    [StepArgumentTransformation]
    public static ComposableEvaluatable SingleEvaluatableTransform(Table table)
        => table.CreateInstance<ComposableEvaluatableModel>().ToEvaluatable();
}
