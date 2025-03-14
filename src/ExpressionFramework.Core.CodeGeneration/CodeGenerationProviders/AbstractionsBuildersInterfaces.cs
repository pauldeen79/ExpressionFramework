namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsBuildersInterfaces(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetBuilderInterfaces(GetAbstractionsInterfaces(), "ExpressionFramework.Core.Builders.Abstractions", "ExpressionFramework.Core.Abstractions", "ExpressionFramework.Core.Builders.Abstractions");

    public override string Path => "ExpressionFramework.Core/Builders/Abstractions";
    
    protected override bool EnableEntityInheritance => true;
}
