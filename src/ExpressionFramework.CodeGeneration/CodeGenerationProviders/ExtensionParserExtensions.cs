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
        var types = new[]
        {
            typeof(IExpression),
            typeof(Models.IAggregator),
            typeof(IOperator),
            typeof(IEvaluatable),
        };

        var resultsArray = await Task.WhenAll(types.Select(GetOverrideModels));
        var results = types
            .Zip(resultsArray, (key, result) => new { key, result })
            .ToDictionary(x => x.key, x => x.result);

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
                        .AddStringCodeStatements(results[typeof(IExpression)].Value!
                            .SelectMany(x => new[]
                            {
                                $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();",
                                $"services.AddSingleton<{Constants.Namespaces.Parser}.Contracts.IExpressionResolver, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();"
                            })
                        )
                        .AddStringCodeStatements(results[typeof(Models.IAggregator)].Value!
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserAggregatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements(results[typeof(IOperator)].Value!
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserOperatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements(results[typeof(IEvaluatable)].Value!
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserEvaluatableResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements("return services;")
                        )
                .Build()
        ]);
    }
}
