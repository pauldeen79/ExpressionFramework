namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class OverrideModels : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Paths.ExpressionModels;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IExpression)),
            Constants.Namespaces.DomainExpressions,
            CurrentNamespace);
}
