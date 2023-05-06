namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IExpression)),
            new(
                Constants.Namespaces.DomainBuilders,
                nameof(ExpressionBuilderFactory),
                Constants.TypeNames.Expression,
                Constants.Namespaces.DomainBuildersExpressions,
                Constants.Types.ExpressionBuilder,
                Constants.Namespaces.DomainExpressions
            ),
            $"if (instance is {Constants.Namespaces.DomainContracts}.{nameof(IUntypedExpressionProvider)} provider) instance = provider.ToUntyped();"
        );
}
