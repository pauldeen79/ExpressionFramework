﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AggregatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(Models.IAggregator)),
            new(
                Constants.Namespaces.DomainBuilders,
                nameof(AggregatorBuilderFactory),
                Constants.TypeNames.Aggregator,
                Constants.Namespaces.DomainBuildersAggregators,
                Constants.Types.AggregatorBuilder,
                Constants.Namespaces.DomainAggregators
            )
        );
}
