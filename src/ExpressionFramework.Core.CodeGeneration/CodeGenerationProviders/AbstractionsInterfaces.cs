namespace ClassFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractionsInterfaces(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetEntityInterfaces(GetAbstractionsInterfaces(), "ExpressionFramework.Core", "ExpressionFramework.Core.Abstractions");

    public override string Path => "ExpressionFramework.Core/Abstractions";

    protected override bool EnableEntityInheritance => true;
}
