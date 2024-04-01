namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    public override IEnumerable<TypeBase> Model
        => GetOverrideModels(typeof(IOperator))
            .Select(x => CreateParserClass(x, Constants.Types.Operator, x.WithoutInterfacePrefix(), Constants.Namespaces.DomainOperators).Build());
}
