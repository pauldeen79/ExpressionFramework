namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class OverrideBuilders : ExpressionFrameworkCSharpClassBase
{
    public OverrideBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.AggregatorBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool CreateAsObservable => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(Models.IAggregator), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in aggregators, so this is not necessary

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetOverrideModels(typeof(Models.IAggregator)), CurrentNamespace, Constants.Namespaces.DomainAggregators);
}
