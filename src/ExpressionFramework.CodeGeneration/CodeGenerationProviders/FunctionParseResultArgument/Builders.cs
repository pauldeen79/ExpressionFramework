namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArgument;

[ExcludeFromCodeCoverage]
public class Builders : CSharpClassBase
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

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName == $"{RecordConcreteCollectionType.WithoutGenerics()}<{typeof(CrossCutting.Utilities.Parsers.FunctionParseResultArgument).FullName}>")
        {
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                RecordConcreteCollectionType.WithoutGenerics(),
                ReplaceWithBuilderNamespaces(typeName).ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture)
            );
        }
        else
        {
            base.FixImmutableBuilderProperty(property, typeName);
        }
    }

    public override object CreateModel()
        => GetImmutableBuilderClasses(new[] { typeof(FunctionParseResult) }, ProjectName, CurrentNamespace);
}
