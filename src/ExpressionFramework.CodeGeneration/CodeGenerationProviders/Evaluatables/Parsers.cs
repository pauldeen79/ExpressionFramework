namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;

    public override async Task<IEnumerable<TypeBase>> GetModel()
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IEvaluatable)))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Evaluatable,
                x.WithoutInterfacePrefix(),
                Constants.Namespaces.DomainEvaluatables,
                settings
            ).Build());
    }
}
