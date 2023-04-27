namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideEvaluatableBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.EvaluatableBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IEvaluatable), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IEvaluatable)),
            Constants.Namespaces.DomainEvaluatables,
            CurrentNamespace);
}
