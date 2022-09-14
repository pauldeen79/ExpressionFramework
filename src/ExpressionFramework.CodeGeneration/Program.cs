﻿using ModelFramework.Common.Extensions;

namespace ExpressionFramework.CodeGeneration;

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

        GenerateCode.For<AbstractExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractNonGenericExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractExpressionEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideExpressionEntities>(settings, multipleContentBuilder);
        GenerateCode.For<ExpressionBuilderFactory>(settings, multipleContentBuilder);

        GenerateCode.For<AbstractOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractNonGenericOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractOperatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideOperatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OperatorBuilderFactory>(settings, multipleContentBuilder);

        GenerateCode.For<Expressions>(settings, multipleContentBuilder);
        GenerateCode.For<Operators>(settings, multipleContentBuilder);

        // Log output to console
        if (string.IsNullOrEmpty(basePath))
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

        FixScaffoldedFileNamesFor<Expressions>(basePath);
        FixScaffoldedFileNamesFor<Operators>(basePath);
    }

    private static void FixScaffoldedFileNamesFor<T>(string basePath)
    where T : ICodeGenerationProvider, new()
    {
        var gen = new T();

        foreach (var file in Directory.GetFiles(Path.Combine(basePath, gen.Path), "*.generated.cs").Where(x => !x.EndsWith(".template.generated.cs", StringComparison.InvariantCulture)))
        {
            File.Move(file, file.ReplaceSuffix(".generated.cs", ".cs", StringComparison.InvariantCulture));
        }
    }
}
