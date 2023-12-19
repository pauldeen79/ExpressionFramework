namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class OperatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(IOperator)),
            new(
                Constants.Namespaces.DomainBuilders,
                nameof(OperatorBuilderFactory),
                Constants.TypeNames.Operator,
                Constants.Namespaces.DomainBuildersOperators,
                Constants.Types.OperatorBuilder,
                Constants.Namespaces.DomainOperators
            )
        );
}
