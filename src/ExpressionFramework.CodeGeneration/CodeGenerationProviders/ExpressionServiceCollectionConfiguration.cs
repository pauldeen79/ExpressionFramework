namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionServiceCollectionConfiguration : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Extensions";
    public override string DefaultFileName => "ServiceCollectionExtensions.generated.cs";
    protected override string FileNameSuffix => ".ExpressionHandlers.template.generated";

    public override object CreateModel()
        => CreateServiceCollectionExtensions(
            "ExpressionFramework.Domain.Extensions",
            "ServiceCollectionExtensions",
            "AddExpressionHandlers",
            GetOverrideExpressionModels(),
            x => $".AddSingleton<IExpressionHandler, {x.Name}Handler>()");
}
