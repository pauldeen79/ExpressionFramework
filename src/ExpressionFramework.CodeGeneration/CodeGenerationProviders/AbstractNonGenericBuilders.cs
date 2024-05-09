
namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractNonGenericBuilders : ExpressionFrameworkCSharpClassBase
{
    public AbstractNonGenericBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.DomainBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool IsAbstract => true;
    protected override bool CreateAsObservable => true;
    protected override string FilenameSuffix => ".nongeneric.template.generated";

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetNonGenericBuilders(await GetAbstractModels(), Constants.Namespaces.DomainBuilders, Constants.Namespaces.Domain);
}
