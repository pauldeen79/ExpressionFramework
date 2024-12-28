namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.DomainBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool IsAbstract => true;

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetAbstractModels(), Constants.Namespaces.DomainBuilders, Constants.Namespaces.Domain);
}
