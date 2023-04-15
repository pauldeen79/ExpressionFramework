namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string DefaultFileName => string.Empty; // not used because we're using multiple files, but it's abstract so we need to fill ilt

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override string FileNameSuffix => Constants.TemplateGenerated;
    protected override string ProjectName => Constants.ProjectName;
    protected override Type BuilderClassCollectionType => typeof(IEnumerable<>);
    protected override bool AddBackingFieldsForCollectionProperties => true;
    protected override bool AddPrivateSetters => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Shared;
}
