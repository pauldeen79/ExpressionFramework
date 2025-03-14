namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsBuildersInterfaces(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetBuilderInterfaces(GetAbstractionsInterfaces(), Constants.Namespaces.CoreBuildersAbstractions, Constants.Namespaces.CoreAbstractions, Constants.Namespaces.CoreBuildersAbstractions);

    public override string Path => Constants.Paths.CoreBuildersAbstractions;
    
    protected override bool EnableEntityInheritance => true;
}
