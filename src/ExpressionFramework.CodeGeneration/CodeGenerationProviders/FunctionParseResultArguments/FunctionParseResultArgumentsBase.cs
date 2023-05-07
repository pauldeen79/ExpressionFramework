namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public abstract class FunctionParseResultArgumentsBase : CSharpClassBase
{
    public override string Path => "ExpressionFramework.Parser.Tests/Builders";
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

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName == typeof(IFormatProvider).FullName)
        {
            property.SetDefaultValueForBuilderClassConstructor(new Literal($"{typeof(CultureInfo).FullName}.{nameof(CultureInfo.InvariantCulture)}"));
        }
        base.FixImmutableBuilderProperty(property, typeName);
    }
}
