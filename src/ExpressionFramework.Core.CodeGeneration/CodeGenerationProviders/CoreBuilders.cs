namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreBuilders(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.CoreBuilders;

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetCoreModels(), Constants.Namespaces.CoreBuilders, Constants.Namespaces.Core);

    protected override bool CreateAsObservable => true;
}
