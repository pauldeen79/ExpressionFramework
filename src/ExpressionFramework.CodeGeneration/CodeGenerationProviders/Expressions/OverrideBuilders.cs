namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class OverrideBuilders : ExpressionFrameworkCSharpClassBase
{
    public OverrideBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ExpressionBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool CreateAsObservable => true;
    protected override async Task<TypeBase?> GetBaseClass() => await CreateBaseClass(typeof(IExpression), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetBuilders(await GetOverrideModels(typeof(IExpression)), CurrentNamespace, Constants.Namespaces.DomainExpressions);
}
