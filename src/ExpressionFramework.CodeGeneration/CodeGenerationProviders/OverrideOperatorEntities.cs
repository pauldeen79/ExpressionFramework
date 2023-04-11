namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Operators";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), Constants.Namespaces.Domain);
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IOperator)), CurrentNamespace);
}
