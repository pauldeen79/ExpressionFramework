﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override IEnumerable<TypeBase> Model
    {
        get
        {
            var settings = CreateSettings();
            return GetOverrideModels(typeof(IExpression)).Result
                .Select(x => CreateParserClass
                (
                    x,
                    Constants.Types.Expression,
                    x.WithoutInterfacePrefix().ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                    Constants.Namespaces.DomainExpressions,
                    settings
                ).Build());
        }
    }
}
