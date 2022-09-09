namespace ExpressionFramework.Abstractions.Tests;

public class ModelGenerationTests
{
    private static readonly CodeGenerationSettings Settings = new CodeGenerationSettings
    (
        basePath: Path.Combine(Directory.GetCurrentDirectory(), @"../../../../"),
        generateMultipleFiles: false,
        skipWhenFileExists: false,
        dryRun: true
    );

    [Fact]
    public void Can_Generate_Records_From_Model()
    {
        var multipleContentBuilder = new MultipleContentBuilder(Settings.BasePath);
        GenerateCode.For<BaseAbstractionsInterfacesModels>(Settings, multipleContentBuilder);
        GenerateCode.For<CoreAbstractionsInterfacesModels>(Settings, multipleContentBuilder);
        Verify(multipleContentBuilder);
    }

    private static void Verify(MultipleContentBuilder multipleContentBuilder)
    {
        var actual = multipleContentBuilder.ToString();

        // Assert
        actual.NormalizeLineEndings().Should().NotBeNullOrEmpty();
    }
}
