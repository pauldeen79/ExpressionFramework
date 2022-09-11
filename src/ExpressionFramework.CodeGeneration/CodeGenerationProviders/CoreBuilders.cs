namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreBuilders : ExpressionFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => GetImmutableBuilderClasses
        (
            GetCoreModels(),
            "ExpressionFramework.Domain",
            "ExpressionFramework.Domain.Tests.Support.Builders"
        );
}
