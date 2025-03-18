namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Functions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.EvaluatableFunctions;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IEvaluatableBase)))
            .OnSuccess(result => Result.Success(result.Value!.SelectMany(x => new TypeBase[] { CreateEvaluatableFunction(x), CreateEvaluatableEvaluationFunction(x) })));

    private TypeBase CreateEvaluatableFunction(TypeBase typeBase)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.WithoutInterfacePrefix()}Function")
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
                    .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){GetFunctionArgumentsAddString(typeBase)}.Build().OnSuccess(results => {typeof(Result).FullName}.Success<ExpressionFramework.Core.Abstractions.IEvaluatable>(new {typeBase.WithoutInterfacePrefix()}({GetFunctionArgumentsGetString(typeBase)})));")
            )
            .AddAttributes(GetFunctionAttributes(typeBase))
            .Build();

    private TypeBase CreateEvaluatableEvaluationFunction(TypeBase typeBase)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"Evaluate{typeBase.WithoutInterfacePrefix().ReplaceSuffix("Evaluatable", string.Empty, StringComparison.Ordinal)}Function")
            .AddInterfaces(typeof(ITypedFunction<>).ReplaceGenericTypeName(typeof(bool)))
            .AddMethods(
                new MethodBuilder()
                    .WithName("Evaluate")
                    .AddParameter("context", typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("System.Object?"))
                    .AddStringCodeStatements("return EvaluateTyped(context).Transform<object?>(x => x);"),
                new MethodBuilder()
                    .WithName("EvaluateTyped")
                    .AddParameter("context", typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName(typeof(bool)))
                    .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){GetFunctionArgumentsAddString(typeBase)}.Build().OnSuccess(results => new {typeBase.WithoutInterfacePrefix()}({GetFunctionArgumentsGetString(typeBase)}).Evaluate(context.Context));")
            )
            .AddAttributes(GetFunctionAttributes(typeBase))
            .Build();
}
