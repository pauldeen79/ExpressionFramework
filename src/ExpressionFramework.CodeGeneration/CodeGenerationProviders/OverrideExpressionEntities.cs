namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideExpressionEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Expressions";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), Constants.Namespaces.Domain);

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IExpression)), CurrentNamespace);
}
