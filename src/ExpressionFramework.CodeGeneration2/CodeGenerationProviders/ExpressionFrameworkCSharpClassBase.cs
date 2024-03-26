namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class ExpressionFrameworkCSharpClassBase : CsharpClassGeneratorPipelineCodeGenerationProviderBase
{
    protected ExpressionFrameworkCSharpClassBase(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string LastGeneratedFilesFilename => string.Empty;
    public override Encoding Encoding => Encoding.UTF8;

    protected override Type EntityCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type EntityConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override Type BuilderCollectionType => typeof(ObservableCollection<>);

    protected override string ProjectName => "ExpressionFramework";
    protected override string CoreNamespace => "ExpressionFramework.Domain";
    protected override bool CopyAttributes => true;
    protected override bool CopyInterfaces => true;
    protected override bool CreateRecord => true;

    protected TypeBase[] GetTemplateFrameworkModels()
        => GetNonCoreModels($"{CodeGenerationRootNamespace}.Models.TemplateFramework");

    protected override bool IsAbstractType(Type type)
    {
        type = type.IsNotNull(nameof(type));

        if (type.IsInterface && type.Namespace == $"{CodeGenerationRootNamespace}.Models" && type.Name.Substring(1).In(Constants.Types.Aggregator, Constants.Types.Evaluatable, Constants.Types.Expression, Constants.Types.Operator))
        {
            return true;
        }

        return base.IsAbstractType(type);
    }
}
