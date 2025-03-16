namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Functions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.EvaluatableFunctions;

    //protected override string FilenameSuffix => string.Empty;
    //protected override bool CreateCodeGenerationHeader => false;
    //protected override bool SkipWhenFileExists => true; // scaffold instead of generate
    //protected override bool GenerateMultipleFiles => true;
    //protected override bool EnableNullablePragmas => false;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IEvaluatableBase)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.WithoutInterfacePrefix()}Function")
                .AddInterfaces(typeof(ITypedFunction<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IEvaluatable"))
                .AddMethods(
                    new MethodBuilder()
                        .WithName("Evaluate")
                        .AddParameter("context", typeof(FunctionCallContext))
                        .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("System.Object?"))
                        .AddStringCodeStatements("return EvaluateTyped(context).Transform<object?>(x => x);"),
                    new MethodBuilder()
                        .WithName("EvaluateTyped")
                        .AddParameter("context", typeof(FunctionCallContext))
                        .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IEvaluatable"))
                        .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){AddArguments(x)}.Build().OnSuccess(results => {typeof(Result).FullName}.Success<ExpressionFramework.Core.Abstractions.IEvaluatable>(new {x.WithoutInterfacePrefix()}Builder(){GetArguments(x)}.Build()));")
                )
                .Build())));

    private string AddArguments(TypeBase typeBase)
    {
        //.Add("InnerEvaluatable", () => context.GetArgumentValueResult<IEvaluatable>(0, "InnerEvaluatable"))
        var builder = new StringBuilder();
        var counter = 0;
        foreach (var prop in typeBase.Properties)
        {
            builder.Append(@$".Add(""{prop.Name}"", () => context.GetArgumentValueResult<{prop.TypeName.GetClassName()}>({counter}, ""{prop.Name}""))");
            counter++;
        }

        return builder.ToString();
    }

    private string GetArguments(TypeBase typeBase)
    {
        //.WithInnerEvaluatable(results.GetValue<IEvaluatable>("InnerEvaluatable").ToBuilder())
        var builder = new StringBuilder();
        foreach (var prop in typeBase.Properties)
        {
            var suffix = prop.TypeName.GetClassName() == "IEvaluatable"
                ? ".ToBuilder()"
                : string.Empty;

            builder.Append($@".With{prop.Name}(results.GetValue<{prop.TypeName.GetClassName()}>(""{prop.Name}""){suffix})");
        }

        return builder.ToString();
    }
}
