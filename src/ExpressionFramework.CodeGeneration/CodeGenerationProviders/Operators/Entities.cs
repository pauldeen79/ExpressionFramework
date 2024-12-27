namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.Operators;
    
    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override bool SkipWhenFileExists => true; // scaffold instead of generate
    protected override bool GenerateMultipleFiles => true;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IOperator)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.WithoutInterfacePrefix())
                .WithPartial()
                .WithRecord()
                .AddMethods(new MethodBuilder()
                    .WithName("Evaluate")
                    .WithProtected()
                    .WithOverride()
                    .AddParameter("leftValue", typeof(object), isNullable: true)
                    .AddParameter("rightValue", typeof(object), isNullable: true)
                    .WithReturnType(typeof(Result<bool>))
                    .NotImplemented()
                )
                .Build())));
}
