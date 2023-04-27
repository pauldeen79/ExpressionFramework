namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExtenstionParserExtensions : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.Parser;

    public override object CreateModel()
        => new[]
        {
            new ClassBuilder()
                .WithStatic()
                .WithNamespace(Constants.Namespaces.Parser)
                .WithName("ServiceCollectionExtensions")
                .AddMethods(
                    new ClassMethodBuilder()
                        .WithStatic()
                        .WithExtensionMethod()
                        .WithName("AddExpressionParsers")
                        .AddParameter("services", typeof(IServiceCollection))
                        .WithType(typeof(IServiceCollection))
                        .AddLiteralCodeStatements(GetOverrideModels(typeof(IExpression)).Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserFunctionResultParsers}.{x.Name}Parser>();"))
                        .AddLiteralCodeStatements("return services;")
                ).Build()
        };
}
