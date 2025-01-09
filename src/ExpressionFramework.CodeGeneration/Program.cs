﻿namespace ExpressionFramework.CodeGeneration;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task Main(string[] args)
    {
        // Setup code generation
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.EndsWith("ExpressionFramework")
            ? Path.Combine(currentDirectory, @"src/")
            : Path.Combine(currentDirectory, @"../../../../");
        var services = new ServiceCollection()
            .AddParsers()
            .AddClassFrameworkPipelines()
            .AddScoped<IBuilderComponentBuilder, BuilderComponents.ExpressionBuilderComponentBuilder>()
            .AddScoped<IBuilderComponentBuilder, BuilderComponents.RemoveExpressionNameAttributeComponentBuilder>()
            .AddScoped<IBuilderComponentBuilder, BuilderComponents.TypedExpressionBuilderComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, EntityComponents.RemoveExpressionNameAttributeComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, EntityComponents.TypedExpressionEntityComponentBuilder>()
            .AddTemplateFramework()
            .AddTemplateFrameworkChildTemplateProvider()
            .AddTemplateFrameworkCodeGeneration()
            .AddTemplateFrameworkRuntime()
            .AddCsharpExpressionDumper()
            .AddClassFrameworkTemplates()
            .AddExpressionParser()
            .AddScoped<IAssemblyInfoContextService, MyAssemblyInfoContextService>();

        var generators = typeof(Program).Assembly.GetExportedTypes()
            .Where(x => !x.IsAbstract && x.BaseType == typeof(ExpressionFrameworkCSharpClassBase))
            .ToArray();

        foreach (var type in generators)
        {
            services.AddScoped(type);
        }

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var engine = scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Generate code
        await Task.WhenAll(generators
            .Select(x => (ExpressionFrameworkCSharpClassBase)scope.ServiceProvider.GetRequiredService(x))
            .Select(x => engine.Generate(x, new MultipleStringContentBuilderEnvironment(), new CodeGenerationSettings(basePath, Path.Combine(x.Path, $"{x.GetType().Name}.template.generated.cs")))));

        // Log output to console
        if (!string.IsNullOrEmpty(basePath))
        {
            Console.WriteLine($"Code generation completed, check the output in {basePath}");
        }
    }
}
