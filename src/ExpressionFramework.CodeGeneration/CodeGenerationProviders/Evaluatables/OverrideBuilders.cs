namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class OverrideBuilders : ExpressionFrameworkCSharpClassBase
{
    public OverrideBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.EvaluatableBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool CreateAsObservable => true;
    protected override async Task<TypeBase?> GetBaseClass() => await CreateBaseClass(typeof(IEvaluatable), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetBuilders(await GetOverrideModels(typeof(IEvaluatable)), CurrentNamespace, Constants.Namespaces.DomainEvaluatables);
}
