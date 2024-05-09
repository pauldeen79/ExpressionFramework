namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreBuilders : ExpressionFrameworkCSharpClassBase
{
    public CoreBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.DomainBuilders;

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetBuilders(await GetCoreModels(), Constants.Namespaces.DomainBuilders, Constants.Namespaces.Domain);
}
