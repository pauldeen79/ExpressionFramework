namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Functions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.EvaluatableFunctions;

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
                        .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){GetFunctionArgumentsAddString(x)}.Build().OnSuccess(results => {typeof(Result).FullName}.Success<ExpressionFramework.Core.Abstractions.IEvaluatable>(new {x.WithoutInterfacePrefix()}({GetFunctionArgumentsGetString(x)})));")
                )
                .AddAttributes(GetFunctionAttributes(x))
                .Build())));
}
