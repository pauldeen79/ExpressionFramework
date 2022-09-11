namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorServiceCollectionConfiguration : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Extensions";
    public override string DefaultFileName => "ServiceCollectionExtensions.generated.cs";
    protected override string FileNameSuffix => ".OperatorHandlers.template.generated";

    public override object CreateModel()
        => new[] { new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.Extensions")
            .WithName("ServiceCollectionExtensions")
            .WithStatic()
            .WithPartial()
            .AddMethods(new ClassMethodBuilder()
                .WithVisibility(Visibility.Private)
                .WithStatic()
                .WithName("AddOperatorHandlers")
                .WithExtensionMethod()
                .WithType(typeof(IServiceCollection))
                .AddParameter("serviceCollection", typeof(IServiceCollection))
                .AddLiteralCodeStatements("return serviceCollection")
                .AddLiteralCodeStatements(GetOverrideOperatorModels().Select(x => $".AddSingleton<IOperatorHandler, {x.Name}Handler>()"))
                .AddLiteralCodeStatements(";")
            )
            .Build() };
}
