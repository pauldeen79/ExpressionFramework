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
        var generateMultipleFiles = true;
        var dryRun = false;
        var multipleContentBuilder = new MultipleContentBuilder { BasePath = basePath };
        var settings = new CodeGenerationSettings(basePath, generateMultipleFiles, false, dryRun);

        // Generate code
        GenerateCode.For<CoreBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<CoreEntities>(settings, multipleContentBuilder);
        GenerateCode.For<RequestBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<RequestEntities>(settings, multipleContentBuilder);

        GenerateCode.For<AbstractExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractNonGenericExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractExpressionEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideExpressionEntities>(settings, multipleContentBuilder);
        GenerateCode.For<ExpressionBuilderFactory>(settings, multipleContentBuilder);
        GenerateCode.For<ExpressionServiceCollectionConfiguration>(settings, multipleContentBuilder);

        GenerateCode.For<AbstractOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractNonGenericOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractOperatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideOperatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OperatorBuilderFactory>(settings, multipleContentBuilder);
        GenerateCode.For<OperatorServiceCollectionConfiguration>(settings, multipleContentBuilder);

        GenerateCode.For<ExpressionHandlers>(settings, multipleContentBuilder).Chain(() => FixScaffoldedFileNamesFor<ExpressionHandlers>(basePath));
        GenerateCode.For<OperatorHandlers>(settings, multipleContentBuilder).Chain(() => FixScaffoldedFileNamesFor<OperatorHandlers>(basePath));

        // Log output to console
#pragma warning disable S2589 // Boolean expressions should not be gratuitous
        if (dryRun || string.IsNullOrEmpty(basePath))
        {
            Console.WriteLine(multipleContentBuilder.ToString());
        }
        else
        {
            Console.WriteLine($"Code generation completed, check the output in {basePath}");
            Console.WriteLine("Generated files:");
            foreach (var content in multipleContentBuilder.Contents)
            {
                Console.WriteLine(content.FileName);
            }
        }
#pragma warning restore S2589 // Boolean expressions should not be gratuitous
    }

    private static void FixScaffoldedFileNamesFor<T>(string basePath)
        where T : ICodeGenerationProvider, new()
    {
        var gen = new T();

        foreach (var file in Directory.GetFiles(Path.Combine(basePath, gen.Path), "*.generated.cs"))
        {
            File.Move(file, file.Replace("generated.cs", "cs"));
        }
    }
}