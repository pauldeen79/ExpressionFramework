namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideExpressionBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainBuilders}/Expressions";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IExpression)),
            $"{Constants.Namespaces.Domain}.Expressions",
            CurrentNamespace);
}
