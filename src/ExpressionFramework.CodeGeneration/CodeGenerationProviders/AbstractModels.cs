namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractModels : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool IsAbstract => true;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetAbstractModels(),
            Constants.Namespaces.Domain,
            Constants.Namespaces.DomainModels);
}
