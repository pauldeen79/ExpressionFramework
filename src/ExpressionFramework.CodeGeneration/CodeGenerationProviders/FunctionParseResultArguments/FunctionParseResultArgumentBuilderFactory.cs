namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public class FunctionParseResultArgumentBuilderFactory : CSharpClassBase
{
    //##
    public override string Path => "ExpressionFramework.Parser.Tests";
    public override string DefaultFileName => string.Empty;
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override string ProjectName => "CrossCutting.Utilities.Parsers";
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override bool EnableNullableContext => true;
    protected override bool CreateCodeGenerationHeader => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None;

    protected override IEnumerable<KeyValuePair<string, string>> GetCustomBuilderNamespaceMapping()
    {
        yield return new KeyValuePair<string, string>(ProjectName, CurrentNamespace);
    }
    //##

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            new[] { typeof(LiteralArgument).ToClass(CreateClassSettings()), typeof(FunctionArgument).ToClass(CreateClassSettings()) },
            new(
                CurrentNamespace,
                nameof(FunctionParseResultArgumentBuilderFactory),
                typeof(FunctionParseResultArgument).FullName!,
                CurrentNamespace,
                "FunctionParseResultArgumentBuilder",
                ProjectName
            )
        );
}
