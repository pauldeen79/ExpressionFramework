namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideAggregatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Aggregators";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IAggregator), Constants.Namespaces.Domain);
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // there are no properties in aggregators, so this is not necessary

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IAggregator)), CurrentNamespace);
}
