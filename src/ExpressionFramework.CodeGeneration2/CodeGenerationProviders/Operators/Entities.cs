namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => $"{Constants.Namespaces.Domain}/{nameof(Operators)}";
    public override string LastGeneratedFilesFilename => string.Empty;

    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override IEnumerable<TypeBase> Model
        => GetOverrideModels(typeof(IOperator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new MethodBuilder()
                    .WithName("Evaluate")
                    .WithProtected()
                    .WithOverride()
                    .AddParameter("leftValue", typeof(object), isNullable: true)
                    .AddParameter("rightValue", typeof(object), isNullable: true)
                    .WithReturnType(typeof(Result<bool>))
                    .NotImplemented()
                )
                .Build());
}
