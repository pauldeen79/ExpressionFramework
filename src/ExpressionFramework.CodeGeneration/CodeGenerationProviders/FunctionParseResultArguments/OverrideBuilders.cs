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
}
