﻿namespace ExpressionFramework.CodeGeneration;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static void Main(string[] args)
    {
        // Setup code generation
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.EndsWith("ExpressionFramework")
            ? Path.Combine(currentDirectory, @"src/")
            : Path.Combine(currentDirectory, @"../../../../");
        var dryRun = false;
        var services = new ServiceCollection()
            .AddParsers()
            .AddPipelines()
            .AddScoped<IBuilderFeatureBuilder, BuilderFeatures.ExpressionBuilderBuilderFeatureBuilder>()
            .AddScoped<IEntityFeatureBuilder, EntityFeatures.ExpressionEntityFeatureBuilder>()
            .AddScoped<IEntityFeatureBuilder, EntityFeatures.TypedExpressionEntityFeatureBuilder>()
            .AddTemplateFramework()
            .AddTemplateFrameworkChildTemplateProvider()
            .AddTemplateFrameworkCodeGeneration()
            .AddTemplateFrameworkRuntime()
            .AddCsharpExpressionDumper()
            .AddClassFrameworkTemplates()
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
        var instances = generators
            .Select(x => (ExpressionFrameworkCSharpClassBase)scope.ServiceProvider.GetRequiredService(x))
            .ToArray();
        var engine = scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Generate code
        var count = 0;
        foreach (var instance in instances)
        {
            var codeGenerationSettings = new CodeGenerationSettings(basePath, Path.Combine(instance.Path, $"{instance.GetType().Name}.template.generated.cs"), dryRun);
            var generationEnvironment = instance.CreateGenerationEnvironment();
            engine.Generate(instance, generationEnvironment, codeGenerationSettings);
            count ++;
        }

        // Log output to console
        if (!string.IsNullOrEmpty(basePath))
        {
            Console.WriteLine($"Code generation completed, check the output in {basePath}");
            Console.WriteLine($"Generated files: {count}");
        }
    }
}
