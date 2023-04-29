namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExtenstionParserExtensions : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.Parser;

    public override object CreateModel()
        => new[]
        {
            new ClassBuilder()
                .WithPartial()
                .WithStatic()
                .WithNamespace(Constants.Namespaces.Parser)
                .WithName("ServiceCollectionExtensions")
                .AddMethods(
                    new ClassMethodBuilder()
                        .WithVisibility(Visibility.Private)
                        .WithStatic()
                        .WithExtensionMethod()
                        .WithName("AddExpressionParsers")
                        .AddParameter("services", typeof(IServiceCollection))
                        .WithType(typeof(IServiceCollection))
                        .AddLiteralCodeStatements(GetOverrideModels(typeof(IExpression))
                            .Where(x => !x.Name.StartsWith("TypedDelegate"))
                            .SelectMany(x => new[]
                            {
                                $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserFunctionResultParsers}.{x.Name}Parser>();",
                                $"services.AddSingleton<{Constants.Namespaces.Parser}.Contracts.IExpressionResolver, {Constants.Namespaces.ParserFunctionResultParsers}.{x.Name}Parser>();"
                            })
                        )
                        .AddLiteralCodeStatements(GetOverrideModels(typeof(Models.IAggregator))
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserAggregatorResultParsers}.{x.Name}Parser>();")
                        )
                        .AddLiteralCodeStatements("return services;")
                ).Build()
        };
}
