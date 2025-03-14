namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class OverrideBuilders(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.EvaluatableBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool CreateAsObservable => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IEvaluatableBase), Constants.Namespaces.Core);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.CoreBuilders;

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetOverrideModels(typeof(IEvaluatableBase)), CurrentNamespace, Constants.Namespaces.CoreEvaluatables);
}
