namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class OverrideEntities(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.Evaluatables;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IEvaluatableBase), Constants.Namespaces.Core);

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetOverrideModels(typeof(IEvaluatableBase)), CurrentNamespace);
}
