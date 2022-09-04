namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders/Operators";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNameSpace => "ExpressionFramework.Domain.Tests.Support.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetOverrideOperatorModels(),
                                      "ExpressionFramework.Domain.Operators",
                                      "ExpressionFramework.Domain.Tests.Support.Builders.Operators");
}
