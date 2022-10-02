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

        GenerateCode.For<AbstractBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractNonGenericBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<AbstractEntities>(settings, multipleContentBuilder);

        GenerateCode.For<OverrideExpressionBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideExpressionEntities>(settings, multipleContentBuilder);
        GenerateCode.For<ExpressionBuilderFactory>(settings, multipleContentBuilder);

        GenerateCode.For<OverrideOperatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideOperatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<OperatorBuilderFactory>(settings, multipleContentBuilder);

        GenerateCode.For<OverrideEvaluatableBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideEvaluatableEntities>(settings, multipleContentBuilder);
        GenerateCode.For<EvaluatableBuilderFactory>(settings, multipleContentBuilder);

        GenerateCode.For<OverrideAggregatorBuilders>(settings, multipleContentBuilder);
        GenerateCode.For<OverrideAggregatorEntities>(settings, multipleContentBuilder);
        GenerateCode.For<AggregatorBuilderFactory>(settings, multipleContentBuilder);

        settings = new CodeGenerationSettings(basePath, generateMultipleFiles, true, dryRun);
        GenerateCode.For<Aggregators>(settings, multipleContentBuilder);
        GenerateCode.For<Evaluatables>(settings, multipleContentBuilder);
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
            Console.WriteLine($"Generated files: {multipleContentBuilder.Contents.Count()}");
            foreach (var content in multipleContentBuilder.Contents)
            {
                Console.WriteLine(content.FileName);
            }
        }
    }
}
