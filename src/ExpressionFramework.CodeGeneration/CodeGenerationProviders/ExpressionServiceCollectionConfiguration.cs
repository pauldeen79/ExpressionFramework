namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionServiceCollectionConfiguration : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Extensions";
    public override string DefaultFileName => "ServiceCollectionExtensions.generated.cs";
    protected override string FileNameSuffix => ".ExpressionHandlers.template.generated";

    public override object CreateModel()
        => new[] { new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.Extensions")
            .WithName("ServiceCollectionExtensions")
            .WithStatic()
            .WithPartial()
            .AddMethods(new ClassMethodBuilder()
                .WithVisibility(Visibility.Private)
                .WithStatic()
                .WithName("AddExpressionHandlers")
                .WithExtensionMethod()
                .WithType(typeof(IServiceCollection))
                .AddParameter("serviceCollection", typeof(IServiceCollection))
                .AddLiteralCodeStatements("return serviceCollection")
                .AddLiteralCodeStatements(GetOverrideExpressionModels().Select(x => $".AddSingleton<IExpressionHandler, {x.Name}Handler>()"))
                .AddLiteralCodeStatements(";")
            )
            .Build() };
}
