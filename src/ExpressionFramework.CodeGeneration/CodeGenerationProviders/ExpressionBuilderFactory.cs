namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IExpression)),
            new("ExpressionFramework.Domain.Builders",
            "ExpressionBuilderFactory",
            "ExpressionFramework.Domain.Expression",
            "ExpressionFramework.Domain.Builders.Expressions",
            "ExpressionBuilder",
            "ExpressionFramework.Domain.Expressions"),
            "if (instance is IUntypedExpressionProvider provider) instance = provider.ToUntyped();");
}
