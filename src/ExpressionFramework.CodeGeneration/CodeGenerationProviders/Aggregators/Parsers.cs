namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators.Aggregators;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    public override IEnumerable<TypeBase> Model
    {
        get
        {
            var settings = CreateSettings();
            return GetOverrideModels(typeof(Models.IAggregator)).Result
                .Select(x => CreateParserClass(x, Constants.Types.Aggregator, x.WithoutInterfacePrefix(), Constants.Namespaces.DomainAggregators, settings).Build());
        }
    }
}
