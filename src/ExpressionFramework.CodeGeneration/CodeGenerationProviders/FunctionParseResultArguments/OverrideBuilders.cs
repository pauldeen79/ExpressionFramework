namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public class OverrideBuilders : CSharpClassBase
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

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(FunctionParseResultArgument), ProjectName);
    protected override string BaseClassBuilderNamespace => CurrentNamespace;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            new[] { typeof(LiteralArgument), typeof(FunctionArgument) },
            ProjectName,
            base.CurrentNamespace);

    protected override bool IsMemberValid(IParentTypeContainer parent, ITypeBase typeBase)
        => parent is not null
        && typeBase is not null
        && (string.IsNullOrEmpty(parent.ParentTypeFullName)
            || parent.ParentTypeFullName.GetClassName() == typeBase.Name
            || GetModelAbstractBaseTyped().Any(x => x == parent.ParentTypeFullName.GetClassName())
            || (BaseClass is not null && typeBase.Name == BaseClass.Name));
}
