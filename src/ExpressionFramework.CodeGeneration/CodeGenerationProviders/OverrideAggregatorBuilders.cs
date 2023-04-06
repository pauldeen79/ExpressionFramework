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
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // there are no properties in aggregators, so this is not necessary

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IAggregator)),
            "ExpressionFramework.Domain.Aggregators",
            "ExpressionFramework.Domain.Builders.Aggregators");
}
