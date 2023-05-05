namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public class OverrideBuilders : FunctionParseResultArgumentsBase
{
    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(FunctionParseResultArgument), ProjectName);
    protected override string BaseClassBuilderNamespace => CurrentNamespace;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            new[] { typeof(LiteralArgument), typeof(FunctionArgument) },
            ProjectName,
            base.CurrentNamespace);

    //TODO: Review if the implementation in the base class is logical, or should support both interfaces and classes. I had to remove $"I{typeBase.Name}" in this override to make the code generation work...
    protected override bool IsMemberValid(IParentTypeContainer parent, ITypeBase typeBase)
        => parent is not null
        && typeBase is not null
        && (string.IsNullOrEmpty(parent.ParentTypeFullName)
            || parent.ParentTypeFullName.GetClassName() == typeBase.Name
            || GetModelAbstractBaseTyped().Any(x => x == parent.ParentTypeFullName.GetClassName())
            || (BaseClass is not null && typeBase.Name == BaseClass.Name));
}
