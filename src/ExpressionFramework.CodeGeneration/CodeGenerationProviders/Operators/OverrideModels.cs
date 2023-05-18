namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class OverrideModels : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Paths.OperatorModels;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainModels;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IOperator)),
            Constants.Namespaces.DomainOperators,
            CurrentNamespace);
}
