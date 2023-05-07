namespace ExpressionFramework.CodeGeneration;

[ExcludeFromCodeCoverage]
internal static class Program
{
    [ExcludeFromCodeCoverage]
    private class LineCountInfo
    {
        public string? Directory { get; set; }
        public long LineCountGenerated { get; set; }
        public long LineCountNotGenerated { get; set; }
        public long LineCountTotal { get; set; }
    }

    private static void Main(string[] args)
    {
        // Setup code generation
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory switch
        {
            var x when x.EndsWith(Constants.ProjectName) => Path.Combine(currentDirectory, @"src/"),
            var x when x.EndsWith(Constants.Namespaces.Domain) => Path.Combine(currentDirectory, @"../"),
            var x when x.EndsWith($"{Constants.ProjectName}.CodeGeneration") => Path.Combine(currentDirectory, @"../"),
            _ => Path.Combine(currentDirectory, @"../../../../")
        };
        var generateMultipleFiles = true;
        var dryRun = false;
        var multipleContentBuilder = new MultipleContentBuilder { BasePath = basePath };
        var settings = new CodeGenerationSettings(basePath, generateMultipleFiles, dryRun);

        // Generate code
        var expressionFrameworkGenerators = typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(ExpressionFrameworkCSharpClassBase) && !x.IsAbstract).ToArray();
        _ = expressionFrameworkGenerators.Select(x => (ExpressionFrameworkCSharpClassBase)Activator.CreateInstance(x)!).Select(x => GenerateCode.For(x.GetSettings(settings), multipleContentBuilder, x)).ToArray();

        var modelFrameworkGenerators = typeof(FunctionParseResultArgumentsBase).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(FunctionParseResultArgumentsBase) && !x.IsAbstract).ToArray();
        _ = modelFrameworkGenerators.Select(x => (FunctionParseResultArgumentsBase)Activator.CreateInstance(x)!).Select(x => GenerateCode.For(settings, multipleContentBuilder, x)).ToArray();

        // Log output to console
        if (string.IsNullOrEmpty(basePath))
        {
            Console.WriteLine(multipleContentBuilder.ToString());
        }
        else
        {
            Console.WriteLine($"Code generation completed, check the output in {basePath}");
            Console.WriteLine($"Generated files: {multipleContentBuilder.Contents.Count()}");
            var statistics = Directory.EnumerateDirectories(basePath)
                .Select(x => new LineCountInfo
                {
                    Directory = x.Split('/').Last(),
                    LineCountGenerated = ComputeLineCount(x, true),
                    LineCountNotGenerated = ComputeLineCount(x, false),
                    LineCountTotal = ComputeLineCount(x, null)
                })
                .ToArray();
            Console.WriteLine("Statistics:");
            var dumper = new DataTableDumper<LineCountInfo>(new ColumnNameProvider(), new ColumnDataProvider<LineCountInfo>());
            Console.WriteLine(dumper.Dump(statistics));
        }
    }

    private static long ComputeLineCount(string directory, bool? generated)
        => Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories)
            .Where(x => IsGeneratedValid(x, generated))
            .Select(x => File.ReadAllLines(x).Length)
            .Sum();

    private static bool IsGeneratedValid(string fileName, bool? generated)
        => generated switch
        {
            true => fileName.EndsWith(".template.generated.cs"),
            false => !fileName.EndsWith(".template.generated.cs"),
            null => true
        };
}
