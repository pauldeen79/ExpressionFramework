namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class ExpressionModelFactory : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(IExpression)),
            new(
                Constants.Namespaces.DomainModels,
                nameof(ExpressionModelFactory),
                Constants.TypeNames.Expression,
                Constants.Namespaces.DomainModelsExpressions,
                Constants.Types.ExpressionModel,
                Constants.Namespaces.DomainExpressions
            ),
            $"if (instance is {Constants.Namespaces.DomainContracts}.{nameof(IUntypedExpressionProvider)} provider) instance = provider.ToUntyped();"
        );
}
