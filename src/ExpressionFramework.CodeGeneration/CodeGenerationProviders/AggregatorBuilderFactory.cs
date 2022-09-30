namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AggregatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IAggregator)),
            "ExpressionFramework.Domain.Builders",
            "AggregatorBuilderFactory",
            "ExpressionFramework.Domain.Aggregator",
            "ExpressionFramework.Domain.Builders.Aggregators",
            "AggregatorBuilder",
            "ExpressionFramework.Domain.Aggregators");
}
