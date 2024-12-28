namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class OverrideEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.Expressions;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IExpression), Constants.Namespaces.Domain);

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetOverrideModels(typeof(IExpression)), CurrentNamespace);
}
