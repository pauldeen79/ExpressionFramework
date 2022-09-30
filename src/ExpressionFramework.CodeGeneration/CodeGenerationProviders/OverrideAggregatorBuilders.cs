namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideAggregatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders/Aggregators";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IAggregator), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNamespace => "ExpressionFramework.Domain.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideAggregatorModels(),
            "ExpressionFramework.Domain.Aggregators",
            "ExpressionFramework.Domain.Builders.Aggregators");
}
