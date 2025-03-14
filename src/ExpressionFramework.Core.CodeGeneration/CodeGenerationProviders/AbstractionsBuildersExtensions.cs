namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsBuildersExtensions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetBuilderExtensions(GetAbstractionsInterfaces(), Constants.Namespaces.CoreBuildersAbstractions, Constants.Namespaces.CoreAbstractions, Constants.Namespaces.CoreBuildersExtensions);

    public override string Path => Constants.Paths.CoreBuildersExtensions;

    protected override bool EnableEntityInheritance => true;
}
