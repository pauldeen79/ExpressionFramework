namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractOperatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableClasses(GetAbstractOperatorModels(), "ExpressionFramework.Domain");
}
