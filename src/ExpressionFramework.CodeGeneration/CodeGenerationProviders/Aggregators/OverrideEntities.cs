namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class OverrideEntities : ExpressionFrameworkCSharpClassBase
{
    public OverrideEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.Aggregators;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(Models.IAggregator), Constants.Namespaces.Domain);
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in aggregators, so this is not necessary

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetOverrideModels(typeof(Models.IAggregator)), CurrentNamespace);
}
