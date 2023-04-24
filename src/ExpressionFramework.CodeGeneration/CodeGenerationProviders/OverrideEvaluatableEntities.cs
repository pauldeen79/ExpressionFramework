namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideEvaluatableEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecialized}/{nameof(Evaluatables)}";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specialized", string.Empty);
    protected override IClass? BaseClass => CreateBaseclass(typeof(IEvaluatable), Constants.Namespaces.Domain);

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IEvaluatable)), CurrentNamespace);
}
