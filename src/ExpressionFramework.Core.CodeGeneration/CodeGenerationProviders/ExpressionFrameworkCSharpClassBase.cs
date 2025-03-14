namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class ExpressionFrameworkCSharpClassBase(IPipelineService pipelineService) : CsharpClassGeneratorPipelineCodeGenerationProviderBase(pipelineService)
{
    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string LastGeneratedFilesFilename => string.Empty;
    public override Encoding Encoding => Encoding.UTF8;

    protected override Type EntityCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type EntityConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override Type BuilderCollectionType => typeof(ObservableCollection<>);

    protected override string ProjectName => "ExpressionFramework";
    protected override string CoreNamespace => "ExpressionFramework.Core";
    protected override bool CopyAttributes => true;
    protected override bool CopyInterfaces => true;
    protected override bool CreateRecord => true;
    protected override bool GenerateMultipleFiles => false;
    protected override bool EnableGlobalUsings => true;
}
