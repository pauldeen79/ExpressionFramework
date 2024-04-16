namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class CaseTransformations
{
    [StepArgumentTransformation]
    public static Case CaseTransform(Table table)
        => table.CreateInstance<CaseModel>().ToCase();
}
