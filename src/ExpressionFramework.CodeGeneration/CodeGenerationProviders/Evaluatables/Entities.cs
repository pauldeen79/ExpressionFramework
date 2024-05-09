namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.Evaluatables;

    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override bool SkipWhenFileExists => true; // scaffold instead of generate
    protected override bool GenerateMultipleFiles => true;

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => (await GetOverrideModels(typeof(IEvaluatable)))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.WithoutInterfacePrefix())
                .WithPartial()
                .WithRecord()
                .AddMethods(new MethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .WithReturnType(typeof(Result<bool>))
                    .NotImplemented()
                )
                .Build());
}
