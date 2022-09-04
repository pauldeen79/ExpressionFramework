namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Operators";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetAbstractOperatorModels(),
                                      "ExpressionFramework.Domain",
                                      "ExpressionFramework.Domain.Builders");
}
