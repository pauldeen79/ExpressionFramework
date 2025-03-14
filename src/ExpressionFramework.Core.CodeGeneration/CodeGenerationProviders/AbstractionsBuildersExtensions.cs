namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsBuildersExtensions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetBuilderExtensions(GetAbstractionsInterfaces(), "ExpressionFramework.Core.Builders.Abstractions", "ExpressionFramework.Core.Abstractions", "ExpressionFramework.Core.Builders.Extensions");

    public override string Path => "ExpressionFramework.Core/Builders/Extensions";

    protected override bool EnableEntityInheritance => true;
}
