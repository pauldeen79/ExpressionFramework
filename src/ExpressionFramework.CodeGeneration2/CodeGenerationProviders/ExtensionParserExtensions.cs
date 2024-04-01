﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExtensionParserExtensions : ExpressionFrameworkCSharpClassBase
{
    public ExtensionParserExtensions(ICsharpExpressionDumper csharpExpressionDumper, IPipeline<IConcreteTypeBuilder, BuilderContext> builderPipeline, IPipeline<IConcreteTypeBuilder, BuilderExtensionContext> builderExtensionPipeline, IPipeline<IConcreteTypeBuilder, EntityContext> entityPipeline, IPipeline<TypeBaseBuilder, ReflectionContext> reflectionPipeline, IPipeline<InterfaceBuilder, InterfaceContext> interfacePipeline) : base(csharpExpressionDumper, builderPipeline, builderExtensionPipeline, entityPipeline, reflectionPipeline, interfacePipeline)
    {
    }

    public override string Path => Constants.Paths.Parser;

    public override IEnumerable<TypeBase> Model
        =>
        [
            new ClassBuilder()
                .WithPartial()
                .WithStatic()
                .WithNamespace(Constants.Namespaces.Parser)
                .WithName("ServiceCollectionExtensions")
                .AddMethods(
                    new MethodBuilder()
                        .WithVisibility(Visibility.Private)
                        .WithStatic()
                        .WithExtensionMethod()
                        .WithName("AddExpressionParsers")
                        .AddParameter("services", typeof(IServiceCollection))
                        .WithReturnType(typeof(IServiceCollection))
                        .AddStringCodeStatements(GetOverrideModels(typeof(IExpression))
                            .SelectMany(x => new[]
                            {
                                $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();",
                                $"services.AddSingleton<{Constants.Namespaces.Parser}.Contracts.IExpressionResolver, {Constants.Namespaces.ParserExpressionResultParsers}.{x.WithoutInterfacePrefix()}Parser>();"
                            })
                        )
                        .AddStringCodeStatements(GetOverrideModels(typeof(Models.IAggregator))
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserAggregatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements(GetOverrideModels(typeof(IOperator))
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserOperatorResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements(GetOverrideModels(typeof(IEvaluatable))
                            .Select(x => $"services.AddSingleton<{typeof(IFunctionResultParser).FullName}, {Constants.Namespaces.ParserEvaluatableResultParsers}.{x.WithoutInterfacePrefix()}Parser>();")
                        )
                        .AddStringCodeStatements("return services;")
                ).Build()
        ];
}
