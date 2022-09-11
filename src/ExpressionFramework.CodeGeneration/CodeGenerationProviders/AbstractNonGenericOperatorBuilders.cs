namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractNonGenericOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override string FileNameSuffix => ".nongeneric.template.generated";

    public override object CreateModel()
        => GetImmutableNonGenericBuilderClasses(GetAbstractOperatorModels(),
                                                "ExpressionFramework.Domain",
                                                "ExpressionFramework.Domain.Tests.Support.Builders");
}
