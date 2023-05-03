namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class FunctionParseResultBuilder : CSharpClassBase
{
    public override string Path => "ExpressionFramework.Parser.Tests";
    public override string DefaultFileName => string.Empty;
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override string ProjectName => Constants.ProjectName;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override bool EnableNullableContext => true;
    protected override bool CreateCodeGenerationHeader => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None;

    public override object CreateModel()
        => new[] { CreateBuilder(typeof(FunctionParseResult).ToClass(CreateClassSettings()), Path).Build() };
}
