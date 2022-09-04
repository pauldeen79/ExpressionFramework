﻿namespace CodeGenerationNext
{
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
            var settings = new CodeGenerationSettings(basePath, generateMultipleFiles, dryRun);

            // Generate code
            GenerateCode.For<CoreBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<CoreEntities>(settings, multipleContentBuilder);

            GenerateCode.For<AbstractExpressionBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<AbstractNonGenericExpressionBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<AbstractExpressionEntities>(settings, multipleContentBuilder);
            GenerateCode.For<OverrideExpressionBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<OverrideExpressionEntities>(settings, multipleContentBuilder);

            GenerateCode.For<AbstractOperatorBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<AbstractNonGenericOperatorBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<AbstractOperatorEntities>(settings, multipleContentBuilder);
            GenerateCode.For<OverrideOperatorBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<OverrideOperatorEntities>(settings, multipleContentBuilder);

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
    }
}
