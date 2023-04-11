namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideEvaluatableEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Evaluatables";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IEvaluatable), Constants.Namespaces.Domain);

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IEvaluatable)), CurrentNamespace);
}
