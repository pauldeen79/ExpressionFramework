namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExtensionParserExtensions : ExpressionFrameworkCSharpClassBase
{
    public ExtensionParserExtensions(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserExtensions;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var actions = new Dictionary<string, Task<Result<IEnumerable<TypeBase>>>>
        {
            { nameof(IExpression), GetOverrideModels(typeof(IExpression)) },
            { nameof(Models.IAggregator), GetOverrideModels(typeof(Models.IAggregator)) },
            { nameof(IOperator), GetOverrideModels(typeof(IOperator)) },
            { nameof(IEvaluatable), GetOverrideModels(typeof(IEvaluatable)) }
        };

        var results = new Dictionary<string, Result<IEnumerable<TypeBase>>>();
        foreach (var action in actions)
        {
            var result = await action.Value;
            results.Add(action.Key, result);
            if (!result.IsSuccessful())
            {
                break;
            }
        }
        var error = results
            .Select(x => new { x.Key, x.Value })
            .FirstOrDefault(x => !x.Value.IsSuccessful());

        if (error is not null)
        {
            return error.Value;
        }

        return Result.Success<IEnumerable<TypeBase>>([
            new ClassBuilder()
            .WithPartial()
            .WithStatic()
            .WithNamespace(Constants.Namespaces.ParserExtensions)
            .WithName("ServiceCollectionExtensions")
            .AddMethods(
                new MethodBuilder()
                    .WithVisibility(Visibility.Private)
                    .WithStatic()
                    .WithExtensionMethod()
                    .WithName("AddExpressionParsers")
                    .AddParameter("services", typeof(IServiceCollection))
                    .WithReturnType(typeof(IServiceCollection))
                    .AddStringCodeStatements(results[nameof(IExpression)].Value!
                        .SelectMany(x => new[]
                        {
                            $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();",
                            $"services.AddSingleton<{Constants.Namespaces.Parser}.Contracts.IExpressionResolver, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();"
                        })
                    )
                    .AddStringCodeStatements(results[nameof(Models.IAggregator)].Value!
                        .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserAggregatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                    )
                    .AddStringCodeStatements(results[nameof(IOperator)].Value!
                        .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserOperatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                    )
                    .AddStringCodeStatements(results[nameof(IEvaluatable)].Value!
                        .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserEvaluatableResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                    )
                    .AddStringCodeStatements("return services;")
                    )
                    .Build()
        ]);
    }
}
