namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.Expressions;
    
    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override bool SkipWhenFileExists => true; // scaffold instead of generate

    public override IEnumerable<TypeBase> Model
        => GetOverrideModels(typeof(IExpression))
            .Select(x =>
            {
                var result = new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.WithoutInterfacePrefix())
                .WithPartial()
                .WithRecord()
                .AddMethods(new MethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .NotImplemented()
                );

                var typedInterface = x.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics()).FixTypeName();
                if (!string.IsNullOrEmpty(typedInterface))
                {
                    result
                        .AddMethods(new MethodBuilder()
                        .WithName("EvaluateTyped")
                        .AddParameter("context", typeof(object), isNullable: true)
                        .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{typedInterface.GetGenericArguments()}>")
                        .NotImplemented()
                    );
                }

                return result
                    .AddGenericTypeArguments(x.GenericTypeArguments)
                    .AddGenericTypeArgumentConstraints(x.GenericTypeArgumentConstraints)
                    .Build();
            });
}
