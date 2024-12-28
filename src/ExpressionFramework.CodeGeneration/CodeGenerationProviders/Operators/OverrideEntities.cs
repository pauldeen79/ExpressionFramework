namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class OverrideEntities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.Operators;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override Task<Result<TypeBase>> GetBaseClass() => CreateBaseClass(typeof(IOperator), Constants.Namespaces.Domain);
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in operators, so this is not necessary

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetOverrideModels(typeof(IOperator)), CurrentNamespace);
}
