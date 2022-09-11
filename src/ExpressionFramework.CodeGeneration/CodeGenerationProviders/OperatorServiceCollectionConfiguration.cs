namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorServiceCollectionConfiguration : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Extensions";
    public override string DefaultFileName => "ServiceCollectionExtensions.generated.cs";
    protected override string FileNameSuffix => ".OperatorHandlers.template.generated";

    public override object CreateModel()
        => CreateServiceCollectionExtensions
        (
            "ExpressionFramework.Domain.Extensions",
            "ServiceCollectionExtensions",
            "AddOperatorHandlers",
            GetOverrideOperatorModels(),
            x => $".AddSingleton<IOperatorHandler, {x.Name}Handler>()"
        );
}
