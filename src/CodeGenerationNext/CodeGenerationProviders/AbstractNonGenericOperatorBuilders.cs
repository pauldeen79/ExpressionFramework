namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractNonGenericOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override string FileNameSuffix => ".nongeneric.template.generated";

    public override object CreateModel()
        => GetImmutableNonGenericBuilderClasses(GetAbstractOperatorModels(),
                                                "ExpressionFramework.Domain",
                                                "ExpressionFramework.Domain.Builders");
}
