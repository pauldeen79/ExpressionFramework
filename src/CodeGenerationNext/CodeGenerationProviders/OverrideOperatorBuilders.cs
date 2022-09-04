namespace CodeGenerationNext.CodeGenerationProviders;

public class OverrideOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Operators/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNameSpace => "ExpressionFramework.Domain.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetOverrideOperatorModels(),
                                      "ExpressionFramework.Domain.Operators",
                                      "ExpressionFramework.Domain.Operators.Builders");
}
