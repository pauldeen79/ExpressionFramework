namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreEntities : ExpressionFrameworkCSharpClassBase
{
    public CoreEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.Domain;

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetEntities(await GetCoreModels(), Constants.Namespaces.Domain);
}
