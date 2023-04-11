namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractNonGenericBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override string FileNameSuffix => ".nongeneric.template.generated";

    public override object CreateModel()
        => GetImmutableNonGenericBuilderClasses(
            GetAbstractModels(),
            Constants.Namespaces.Domain,
            Constants.Namespaces.DomainBuilders);
}
