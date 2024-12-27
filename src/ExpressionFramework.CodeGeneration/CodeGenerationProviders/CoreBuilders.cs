namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreBuilders : ExpressionFrameworkCSharpClassBase
{
    public CoreBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.DomainBuilders;

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetCoreModels(), Constants.Namespaces.DomainBuilders, Constants.Namespaces.Domain);

    protected override bool CreateAsObservable => true;
}
