namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class OverrideEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.Evaluatables;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IEvaluatable), Constants.Namespaces.Domain);

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetOverrideModels(typeof(IEvaluatable)), CurrentNamespace);
}
