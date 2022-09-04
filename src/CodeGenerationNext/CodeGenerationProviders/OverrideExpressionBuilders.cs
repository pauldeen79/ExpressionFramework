namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideExpressionBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Expressions/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNameSpace => "ExpressionFramework.Domain.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetOverrideExpressionModels(),
                                      "ExpressionFramework.Domain.Expressions",
                                      "ExpressionFramework.Domain.Expressions.Builders");
}
