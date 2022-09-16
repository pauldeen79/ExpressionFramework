namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideEvaluatableBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders/Evaluatables";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IEvaluatable), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNamespace => "ExpressionFramework.Domain.Tests.Support.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideEvaluatableModels(),
            "ExpressionFramework.Domain.Evaluatables",
            "ExpressionFramework.Domain.Tests.Support.Builders.Evaluatables");
}
