namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IExpression)),
            new(Constants.Namespaces.DomainBuilders,
            nameof(ExpressionBuilderFactory),
            $"{Constants.Namespaces.Domain}.Expression",
            $"{Constants.Namespaces.DomainBuilders}.Expressions",
            "ExpressionBuilder",
            $"{Constants.Namespaces.Domain}.Expressions"),
            $"if (instance is {Constants.Namespaces.Domain}.Contracts.{nameof(IUntypedExpressionProvider)} provider) instance = provider.ToUntyped();");
}
