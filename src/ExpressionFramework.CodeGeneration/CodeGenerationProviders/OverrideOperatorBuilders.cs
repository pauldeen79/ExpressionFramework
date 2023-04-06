namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders/Operators";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNamespace => "ExpressionFramework.Domain.Builders";
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IOperator)),
            "ExpressionFramework.Domain.Operators",
            "ExpressionFramework.Domain.Builders.Operators");
}
