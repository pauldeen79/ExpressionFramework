namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsInterfaces(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetEntityInterfaces(GetAbstractionsInterfaces(), Constants.Namespaces.Core, Constants.Namespaces.CoreAbstractions);

    public override string Path => Constants.Paths.CoreAbstractions;

    protected override bool EnableEntityInheritance => true;
}
