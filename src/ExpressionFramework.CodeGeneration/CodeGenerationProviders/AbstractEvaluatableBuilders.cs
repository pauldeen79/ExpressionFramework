namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractEvaluatableBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetAbstractEvaluatableModels(),
            "ExpressionFramework.Domain",
            "ExpressionFramework.Domain.Tests.Support.Builders");
}
