namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public Entities(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.Expressions;

    protected override string FilenameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override bool SkipWhenFileExists => true; // scaffold instead of generate
    protected override bool GenerateMultipleFiles => true;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IExpression)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x =>
                {
                    var result = new ClassBuilder()
                        .WithNamespace(CurrentNamespace)
                        .WithName(x.WithoutInterfacePrefix())
                        .WithPartial()
                        .WithRecord()
                        .AddMethods(new MethodBuilder()
                            .WithName("Evaluate")
                            .WithOverride()
                            .AddParameter("context", typeof(object), isNullable: true)
                            .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                            .NotImplemented()
                        );

                    var typedInterface = x.Interfaces.FirstOrDefault(x => x is not null && x.WithoutProcessedGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression").FixTypeName();
                    if (!string.IsNullOrEmpty(typedInterface))
                    {
                        result
                            .AddMethods(new MethodBuilder()
                            .WithName("EvaluateTyped")
                            .AddParameter("context", typeof(object), isNullable: true)
                            .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{typedInterface.GetGenericArguments()}>")
                            .NotImplemented()
                        );
                    }

                    return result
                        .AddGenericTypeArguments(x.GenericTypeArguments)
                        .AddGenericTypeArgumentConstraints(x.GenericTypeArgumentConstraints)
                        .Build();
                })));
}
