namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class RequestBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders/Requests";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetRequestModels(),
            "ExpressionFramework.Domain.Requests",
            "ExpressionFramework.Domain.Tests.Support.Builders.Requests");
}
