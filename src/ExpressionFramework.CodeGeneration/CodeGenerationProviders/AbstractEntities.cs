namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.Domain;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override bool IsAbstract => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // not needed for abstract entities, because each derived class will do its own validation

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetAbstractModels(), Constants.Namespaces.Domain);
}
