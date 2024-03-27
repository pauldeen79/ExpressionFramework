﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.Aggregators;

    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override bool SkipWhenFileExists => true; // scaffold instead of generate

    public override IEnumerable<TypeBase> Model
        => GetOverrideModels(typeof(Models.IAggregator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.WithoutInterfacePrefix())
                .WithPartial()
                .WithRecord()
                .AddMethods(new MethodBuilder()
                    .WithName("Aggregate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .AddParameter("secondExpression", typeof(IExpression))
                    .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .NotImplemented()
                )
                .Build());

}
