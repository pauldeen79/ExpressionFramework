namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class OverrideBuilders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.EvaluatableBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool CreateAsObservable => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IEvaluatable), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetOverrideModels(typeof(IEvaluatable)), CurrentNamespace, Constants.Namespaces.DomainEvaluatables);
}
