namespace CodeGeneration
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Setup code generation
            var basePath = args.Length == 1 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), @"../");
            var generateMultipleFiles = true;
            var dryRun = false;
            var multipleContentBuilder = new MultipleContentBuilder { BasePath = basePath };
            var settings = new CodeGenerationSettings(basePath, generateMultipleFiles, false, dryRun);

            // Generate code
            GenerateCode.For<AbstractionsBuildersInterfaces>(settings, multipleContentBuilder);
            GenerateCode.For<AbstractionsExtensionsBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<CoreBuilders>(settings, multipleContentBuilder);
            GenerateCode.For<CoreEntities>(settings, multipleContentBuilder);

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
